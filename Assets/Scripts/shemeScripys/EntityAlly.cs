using UnityEngine;

public class EntityAlly : Entity
{
    protected override void Start()
    {
        base.Start();
        faction.factionType = Faction.FactionType.Ally;
        faction.enemyMask = LayerMask.GetMask("Enemy");
    }

    protected override void HandleMovement()
    {
        // ���������� �������� ��������� (NPC ��� ����������� ���������)
        // ����� �������� ������ ��� ��������� NPC ��� �������� ������ ��������������
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // �������������� ������ ��� ����������� ��������
    }
}