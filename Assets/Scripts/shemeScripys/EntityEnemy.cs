using UnityEngine;

public class EntityEnemy : Entity
{
    [Header("Enemy Settings")]
    public float stoppingDistance = 1f; // ��������� ��������� ����� �����

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

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);

        // ��������� ������ ���� ���� ��� ������� �����
        if (distance > attackRange - stoppingDistance)
        {
            Vector3 direction = (CurrentTarget.position - transform.position).normalized;
            Move(direction);
        }
        else
        {
            Move(Vector3.zero); // ��������������� ��� �����
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