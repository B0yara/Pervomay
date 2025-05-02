using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerEntity : Entity
{
    [Header("Player Controls")]
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public int heavyAttackMultiplier = 2;


    public KeyCode attackKey        =     KeyCode.Z;
    public KeyCode heavyAttackKey   =     KeyCode.X;
    public KeyCode dashKey          =     KeyCode.C;

    private Vector2 moveInput;
    private bool isDashing;
    private bool dashInput;
    private bool attackInput;
    private bool heavyAttackInput;



    protected override void Start()
    {
        base.Start();
        faction.factionType = Faction.FactionType.Player;
        faction.enemyMask = LayerMask.GetMask("Enemy");
    }

    protected override void Update()
    {
        if (isDead) return;

        GetInput();

        if (!isDashing)
        {
            HandleMovement();
            HandleAttacks();
        }

        HandleDash();
    }

    private void GetInput()
    {
        moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
            ).normalized;


        attackInput = Input.GetKeyDown(attackKey);
        heavyAttackInput = Input.GetKeyDown(heavyAttackKey);
        dashInput = Input.GetKeyDown(dashKey);
    }
    protected override void HandleMovement()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // Поворот
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Движение с учетом гравитации
        Vector3 move = direction * moveSpeed;
        move.y = velocity.y;
        controller.Move(move * Time.deltaTime);

        // Анимация движения
        if (animator != null)
            animator.SetFloat(moveAnimParam, direction.magnitude);
    }
    private void HandleAttacks()
    {
        if (attackInput && !isAttacking)
        {
            StartCoroutine(PerformAttack(attackDamage, "Attack"));
        }

        if (heavyAttackInput && !isAttacking)
        {
            StartCoroutine(PerformAttack(attackDamage * heavyAttackMultiplier, "HeavyAttack"));
        }
    }
    private IEnumerator PerformAttack(int damage, string trigger)
    {
        isAttacking = true;
        animator.SetTrigger(trigger);

        // Ожидаем момент удара в анимации (настройте Animation Event)
        yield return new WaitForSeconds(0.3f);

        // Проверка попадания
        if (Physics.Raycast(
            transform.position + Vector3.up * 0.5f,
            transform.forward,
            out var hit,
            attackRange,
            faction.enemyMask))
        {
            hit.collider.GetComponent<_CanDamage>()?.GetDamage(damage);
        }

        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }
    private void HandleDash()
    {
        if (dashInput && !isDashing && !isAttacking)
        {
            StartCoroutine(PerformDash());
        }
    }
    private IEnumerator PerformDash()
    {
        isDashing = true;
        Vector3 dashDirection = moveInput.magnitude > 0.1f
            ? new Vector3(moveInput.x, 0, moveInput.y).normalized
            : transform.forward;

        animator.SetTrigger("Dash");
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            controller.Move(dashDirection * (dashDistance / dashDuration) * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    // Отключаем ненужные AI-методы
    protected override void UpdateAI() { }
    protected override void FindTarget() { }
    protected override bool CanAttack() => false;
}