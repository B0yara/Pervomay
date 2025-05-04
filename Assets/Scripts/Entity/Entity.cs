using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class Entity : _CanDamage
{
    public Faction faction;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float standardMoveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("Combat Settings")]
    public float detectionRange = 10f;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 10;

    
    [Header("Audio Clips"), Space]
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip attackClip1;
    public AudioClip deathClip;


    [Space]
    public CharacterController controller;
    protected Vector3 velocity;
    protected bool isGrounded;
    protected bool isDead = false;
    public Transform currentTarget;
    protected float nextAttackTime;
    public bool isAttacking = false;

    public enum SoundType { Hit, Attack, Death }

    [Header("Target and AI")]
    public Transform CurrentTarget
    {
        get => currentTarget;
        protected set => currentTarget = value;
    }
    // ��� ��������
    public virtual void SetTarget(Transform target)
    {
        currentTarget = target;
        animator.SetInteger("indexAnimation", 1);
    }
    public virtual void StartSource(AudioClip selectedSound)
    {
        audioSource.PlayOneShot(hitClip);
    }

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        animator ??= GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();


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
        else
        {
            animator.SetInteger("indexAnimation", 0);
        }
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
        // ������� �����:
        // 1. ���� ���������� � ����
        // 2. �� �� ��������
        // 3. � ������� �����
        // 4. ��� ����������� ����� ���� � �����

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
        // ������������� ����� ��������� �����
        nextAttackTime = Time.time + 1f / attackRate;
        StartAnimation(2);
    }
    
    // ���������� �� �������� ����� (��� ���������)
    public virtual void OnAttackAnimationHit()
    {
        if (!isAttacking) return;

        // ���������, ��� �� ��� ���� � ���� ���������
        if (CanAttack())
        {
            if (CurrentTarget.TryGetComponent<_CanDamage>(out var damageable))
            {
                damageable.GetDamage(attackDamage);
            }
        }

    }
    // ������������ ���� � ���������
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
        // ������� �������� (���������������� � �����������)
    }
    protected void Move(Vector3 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            // ������� � ����������� ��������
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // ��������
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
