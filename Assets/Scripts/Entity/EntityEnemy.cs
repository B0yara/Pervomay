using UnityEngine;

public class EntityEnemy : Entity
{
    [Header("Enemy Settings")]
    private GameObject damagePrefab;
    protected Transform point;
    

    private Vector3 direction;

    protected override void Start()
    {
        base.Start();
        faction.factionType = Faction.FactionType.Enemy;
        faction.enemyMask = LayerMask.GetMask("Player", "Ally");
        EnemyController.Instance.RegisterEnemy(this);
    }
    protected override void Update()
    {
        base.Update();
        UpdateAI();

    }
    protected override void HandleMovement()
    {
        if (CurrentTarget == null) return;

        UpdateRotation();

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);

        if (animator.GetInteger("indexAnimation") != 2)
            if (distance > attackRange)
            {
                Vector3 moveDirection = (CurrentTarget.position - transform.position).normalized;
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(Vector3.zero); // Останавливаемся для атаки
                animator.SetInteger("indexAnimation", 2);
            }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (EnemyController.Instance != null)
        {
            EnemyController.Instance.UnregisterEnemy(this);
        }
    }
}