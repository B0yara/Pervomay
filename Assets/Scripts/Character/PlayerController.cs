using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 5f;     // Скорость движения
    public float RotationSpeed = 10f; // Скорость поворота
    private float _currentSpeed;      // Текущая скорость

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Combo Settings")]
    [SerializeField] private float comboTimeWindow = 0.5f; // Время между ударами
    [SerializeField] private int maxComboSteps = 3; // Макс. число ударов в комбо
    [SerializeField] private Animator animator;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;

    private int currentComboStep = 0;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;
    private bool CachedAttack = false;


    private Rigidbody rb;
    private Vector3 _movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(attackKey))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (isAttacking)
        {
            CachedAttack = true;
            return; // Если уже атакуем, кэшируем удар
        }

        isAttacking = true;
        animator.SetBool("isAttack", true);
    }

    void EncreseComboStep()
    {
        if (currentComboStep < 2)
        {
            currentComboStep++;
        }
        else
        {
            currentComboStep = 0;
        }
    }

    private void OnBeginAttack(int attackIndex)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        if (hitEnemies.Length == 0) return; // Если нет врагов в радиусе атаки, выходим

        foreach (Collider enemy in hitEnemies)
        {
            // Проверяем, находится ли враг в угле атаки
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToEnemy);

            if (angle < attackAngle / 2)
            {
                if (enemy.TryGetComponent<_CanDamage>(out var damageable))
                {
                    damageable.GetDamage(attackDamage * (currentComboStep + 1)); // Увеличиваем урон в зависимости от комбо
                }
            }
        }
    }

    private void OnEndAttack()
    {
        isAttacking = false;
        if (CachedAttack)
        {
            EncreseComboStep();
            Debug.Log("End Attack: contionue");
            CachedAttack = false;
            Attack(); // Если есть кэшированный удар, выполняем его
        }
        else
        {
            Debug.Log("End Attack: reset");
            animator.SetBool("isAttack", false);
            currentComboStep = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_movement.magnitude > 0.1f) // Если есть движение
        {
            // Перемещение
            rb.linearVelocity = _movement * MoveSpeed;
            // Поворот в сторону движения
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // Остановка, если нет ввода
        }
    }


}