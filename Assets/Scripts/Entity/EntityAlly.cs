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
        // Реализация движения союзников (NPC или управляемых сущностей)
        // Можно оставить пустым для статичных NPC или добавить логику патрулирования
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        // Дополнительная логика при уничтожении союзника
    }
}