using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerEntity : Entity
{
    [Header("Player Controls")]
    private float nextActionTime = 0f;
    public float interval = 5f;

    public KeyCode attackKey        =     KeyCode.Z;
    public KeyCode heavyAttackKey   =     KeyCode.X;
    public KeyCode dashKey          =     KeyCode.C;

    private Vector2 moveInput;
    private bool isDashing;
    private bool dashInput;
    private bool attackInput;
    private bool heavyAttackInput;

    [Header("Zones")]
    public GameObject lighZone;
    public GameObject highZone;


    [Header("AnimationTimings")]
    public float dashSpeedMultiplier = 10f;
    public int heavyAttackMultiplier = 2;

    protected override void Start()
    {
        base.Start();
        faction.factionType = Faction.FactionType.Player;
        faction.enemyMask = LayerMask.GetMask("Enemy");
    }

    protected override void Update()
    {
        base.Update();
        if (isDead) return;

        GetInput();
        if (!isDashing)
        {
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
            animator.SetBool("LighAttack", true);
        }

        if (heavyAttackInput && !isAttacking)
        {
            animator.SetBool("HighAttack", true);
        }
    }
    // Вызывается если всё ок
    private void HandleDash()
    {
        if (dashInput && !isDashing && !isAttacking)
        {
            canDamage = false;
            animator.SetBool("Dash", true);

        }
    }
    private void EndDash()
    {
        animator.SetBool("Dash", false);
        canDamage = true;
    }

    private void LighZone()
    {
        lighZone.SetActive(!lighZone);
        lighZone.GetComponent<ZoneData>().Initialize(attackDamage, faction.enemyMask);
    }
    private void HighZone()
    {
        highZone.SetActive(!highZone);
        highZone.GetComponent<ZoneData>().Initialize(
            attackDamage * heavyAttackMultiplier,
            faction.enemyMask);
    }



    // Отключаем ненужные AI-методы
    protected override void UpdateAI() { }
    protected override void FindTarget() { }
    protected override bool CanAttack() => false;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameController contr = GameObject.Find("GameController").GetComponent<GameController>();
        contr.EndGame();

    }
}