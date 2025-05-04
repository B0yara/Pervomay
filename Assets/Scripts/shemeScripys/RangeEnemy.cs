using UnityEngine;

public class RangeEnemy : EntityEnemy
{
    public float safeDistance = 5f;
    public Projectile projectilePrefab;
    public Transform attackPoint;
    protected override void HandleMovement()
    {
        if (CurrentTarget == null || animator.GetBool("Death")) return;

        UpdateRotation();

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);
        Vector3 moveDirection = Vector3.zero;

        // ��������� ������ ���� ���� ��� ������� �����
        if (distance > attackRange)
        {
            moveDirection = (CurrentTarget.position - transform.position).normalized;
            animator.SetInteger("indexAnimation", 1);
        }
        else if (distance < safeDistance + 2)
        {
            moveDirection = (transform.position - CurrentTarget.position).normalized;
            animator.SetInteger("indexAnimation", 1);
        }
        else
        {
            if (animator.GetInteger("indexAnimation") != 2)
            {
                animator.SetInteger("indexAnimation", 2);
            }
        }
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

    }
    protected override bool CanAttack()
    {
        // ������� �����:
        // 1. ���� ���������� � ����
        // 2. �� �� ��������
        // 3. � ������� �����
        // 4. ��� ����������� ����� ���� � �����

        if (CurrentTarget == null || isDead || isAttacking || animator.GetBool("Death")) return false;
        if (Time.time < nextAttackTime) return false;

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);
        if (distance > attackRange) return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position,
                       (CurrentTarget.position - transform.position).normalized,
                       out hit,
                       attackRange,
                       faction.enemyMask) && distance > safeDistance)
        {
            return hit.transform == CurrentTarget;
        }

        return false;
    }
    protected override void Attack()
    {
        base.Attack();
        LaunchProjectile();
        isAttacking = false;
        Debug.Log($"RangeFire: {nextAttackTime}={Time.time + 1f / attackRate}");
        
    }
    protected virtual void LaunchProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
        projectile.Initialize(attackDamage, CurrentTarget, faction.enemyMask);
    }
}
