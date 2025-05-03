using UnityEngine;

public class MelleEnemy : EntityEnemy
{
    public Transform damagePoint;
    [SerializeField] GameObject damageZonePrefab;
    
    /// <summary>
    /// Когда состояние анимации = 2;
    /// </summary>
    protected override void Attack()
    {
        base.Attack();
        Debug.Log($"MelleFire: {nextAttackTime}={Time.time + 1f / attackRate}");
    }
    protected virtual void CreateDamageZone()
    {
        damageZonePrefab.SetActive(true);
        damageZonePrefab.GetComponent<ZoneData>().Initialize(attackDamage, faction.enemyMask);
    }

    protected virtual void RemoveDamageZone()
    {
        damageZonePrefab.SetActive(false);
    }
}
