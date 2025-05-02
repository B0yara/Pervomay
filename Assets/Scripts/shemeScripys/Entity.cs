using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Entity : _CanDamage
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Animation Settings")]
    public Animator animator;
    public string moveAnimParam = "Speed";
    public string jumpAnimParam = "Jump";
    public string groundedAnimParam = "Grounded";
    public string deathAnimParam = "Die";

    protected CharacterController controller;
    protected Vector3 velocity;
    protected bool isGrounded;
    protected bool isDead = false;

    // Тип сущности
    public enum EntityType { Static, Ally, Enemy }
    public EntityType entityType;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        HandleGravity();
        HandleMovement();
        UpdateAnimations();
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

    protected void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (animator != null && !string.IsNullOrEmpty(jumpAnimParam))
                animator.SetTrigger(jumpAnimParam);
        }
    }

    protected virtual void UpdateAnimations()
    {
        if (animator == null) return;

        // Передаем скорость в аниматор
        float speed = controller.velocity.magnitude;
        animator.SetFloat(moveAnimParam, speed);

        // Передаем состояние "на земле"
        animator.SetBool(groundedAnimParam, isGrounded);
    }

    public override void Die()
    {
        if (isDead) return;

        isDead = true;

        // Анимация смерти
        if (animator != null && !string.IsNullOrEmpty(deathAnimParam))
            animator.SetTrigger(deathAnimParam);
        else if (deathAnimation != null)
            deathAnimation.Play();

        // Отключаем коллайдер и физику
        if (controller != null)
            controller.enabled = false;

        // Уничтожение объекта через время
        Destroy(gameObject, 5f);
    }

    // Метод для проверки типа сущности
    public bool IsEnemy()
    {
        return entityType == EntityType.Enemy;
    }

    public bool IsAlly()
    {
        return entityType == EntityType.Ally;
    }
}