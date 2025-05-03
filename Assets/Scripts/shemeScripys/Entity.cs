using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Entity : _CanDamage
{
    public Faction faction;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("Animation Settings")]
    public string moveAnimParam = "Run";
    public string groundedAnimParam = "Null";
    public string deathAnimParam = "Die";

    [Header("Combat Settings")]
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 10;
    public Projectile projectilePrefab;
    public Transform attackPoint;



    protected CharacterController controller;
    protected Vector3 velocity;
    protected bool isGrounded;
    protected bool isDead = false;
    protected Transform currentTarget;
    protected float nextAttackTime;
    public bool isAttacking = false;


    [Header("Target and AI")]
    public Transform CurrentTarget
    {
        get => currentTarget;
        protected set => currentTarget = value;
    }
    // Тип сущности
    public virtual void SetTarget(Transform target)
    {
        CurrentTarget = target;
        animator.SetInteger("indexAnimation", 1);
    }

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        animator ??= GetComponentInChildren<Animator>();


    }
    protected virtual void Update()
    {
        if (isDead) return;

        HandleGravity();
        HandleMovement();
    }
    protected virtual void UpdateAI()
    {
        FindTarget();
        if (!isAttacking) if (CanAttack()) Attack();

    }
    protected virtual void FindTarget()
    {
        if (CurrentTarget != null && CurrentTarget.gameObject.activeInHierarchy)
            return;

        var closestTarget = FindClosestTarget();
        if (closestTarget != null)
            SetTarget(closestTarget);
    }
    private Transform FindClosestTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, detectionRange, faction.enemyMask);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = target.transform;
            }
        }
        return closest;
    }
    protected virtual bool CanAttack()
    {
        // Условия атаки:
        // 1. Цель существует и жива
        // 2. Не на кулдауне
        // 3. В радиусе атаки
        // 4. Нет препятствий между нами и целью

        if (CurrentTarget == null || isDead || isAttacking) return false;
        if (Time.time < nextAttackTime) return false;

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);
        if (distance > attackRange) return false;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position,
                       (CurrentTarget.position - transform.position).normalized,
                       out hit,
                       attackRange,
                       faction.enemyMask))
        {
            return hit.transform == CurrentTarget;
        }

        return false;
    }
    protected virtual void Attack()
    {
        // Устанавливаем время следующей атаки
        nextAttackTime = Time.time + 1f / attackRate;
        StartAnimation(2);
    }
    protected virtual void LaunchProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
        projectile.Initialize(attackDamage, CurrentTarget, faction.enemyMask);
    }
    // Вызывается из анимации атаки (для ближников)
    public virtual void OnAttackAnimationHit()
    {
        if (!isAttacking) return;

        // Проверяем, все ли еще цель в зоне поражения
        if (CanAttack())
        {
            if (CurrentTarget.TryGetComponent<_CanDamage>(out var damageable))
            {
                damageable.GetDamage(attackDamage);
            }
        }

    }
    // Визуализация луча в редакторе
    private void OnDrawGizmosSelected()
    {
        if (CurrentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (CurrentTarget.position - transform.position).normalized * attackRange);
        }
    }
    protected virtual void HandleMovement()
    {
        // Базовое движение (переопределяется в наследниках)
    }
    protected void Move(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            // Поворот в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Движение
            controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }

    protected virtual void UpdateRotation()
    {
        if (currentTarget == null) return;

        Vector3 direction = (CurrentTarget.position - transform.position).normalized;
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    protected void HandleGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void StartAnimation(int animNumber)
    {
        animator.SetInteger("indexAnimation", animNumber);
        if (animNumber == 2) isAttacking = true;
        else isAttacking = false;
    }
    protected virtual void OnDestroy()
    {

    }

    
}
