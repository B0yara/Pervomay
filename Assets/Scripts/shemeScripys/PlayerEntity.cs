using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;


public class PlayerEntity : Entity
{
    [Header("Player Controls")]
    public float interval = 5f;

    public KeyCode attackKey = KeyCode.Z;
    public KeyCode heavyAttackKey = KeyCode.X;
    public KeyCode dashKey = KeyCode.C;

    private Vector2 moveInput;
    private bool isDashing;
    private bool dashInput;
    private bool attackInput;
    private bool heavyAttackInput;

    [Header("Zones")]
    public GameObject lighZone;
    public GameObject highZone;


    [Header("AnimationTimings")]
    public float dashSpeedMultiplier = 2f;
    public int heavyAttackMultiplier = 2;

    [Header("Canvas")]
    public HPController HPController;

    protected override void Start()
    {
        base.Start();
        HPController.Init(maxHP);
        faction.factionType = Faction.FactionType.Player;
        faction.enemyMask = LayerMask.GetMask("Enemy");
    }
    protected override void Update()
    {
        if (isDead) return;
        base.Update();

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

        if (animator != null)
        {
            if (moveInput != Vector2.zero)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }




        attackInput = Input.GetKeyDown(attackKey);
        //heavyAttackInput = Input.GetKeyDown(heavyAttackKey);
        dashInput = Input.GetKeyDown(dashKey);
    }
    protected override void HandleMovement()
    {
        if (!animator.GetBool("Attack"))
        {
            Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            // �������
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            // �������� � ������ ����������
            Vector3 move = direction * moveSpeed;
            move.y = velocity.y;
            controller.Move(move * Time.deltaTime);

            // �������� ��������
            /*if (animator != null)
                animator.SetBool(moveAnimParam, direction.magnitude > 0.1f);*/
        }

    }
    private void HandleAttacks()
    {
        if (attackInput && !isAttacking)
        {
            animator.SetBool("Attack", true);
        }

        if (heavyAttackInput && !isAttacking)
        {
            animator.SetBool("HighAttack", true);
        }
        if (animator.GetBool("TakeDamage"))
        {
            animator.SetBool("Attack", false);
        }
    }
    public void EndAttack()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;
    }
    // ���������� ���� �� ��
    private void HandleDash()
    {
        // ���� ����� �� ����� � Animator
        if (dashInput && !isDashing && !isAttacking)
        {
            canDamage = false;
            animator.SetBool("Dash", true);

            moveSpeed = standardMoveSpeed * dashSpeedMultiplier;
        }
    }
    public void IsDashing()
    {
        isDashing = true;
        animator.SetBool("isDashing", true);
    }
    private void EndDash()
    {
        isDashing = false;
        animator.SetBool("Dash", false);
        animator.SetBool("isDashing", false);
        canDamage = true;
        moveSpeed = standardMoveSpeed;
    }
    private void LighZone()
    {
        lighZone.SetActive(!lighZone.activeSelf);

        lighZone.GetComponent<ZoneData>().Initialize(attackDamage, faction.enemyMask);
    }
    // ��������� �������� AI-������
    protected override void UpdateAI() { }
    protected override void FindTarget() { }
    protected override bool CanAttack() => false;
    protected override void Die(float time)
    {
        base.Die(time);
        GameController.Instance.GameOver();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    protected override void GetHp()
    {
        HPController.SetValue(hp);
    }
}
