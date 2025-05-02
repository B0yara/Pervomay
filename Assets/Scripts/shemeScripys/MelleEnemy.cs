using UnityEngine;

public class MelleEnemy : EntityEnemy
{
    public Transform damagePoint;
    [SerializeField] GameObject damageZonePrefab;
    private GameObject nowZone;
    
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
        nowZone = Instantiate(damageZonePrefab, damagePoint );
    }

    protected virtual void RemoveDamageZone()
    {
        Destroy(nowZone);
    }
}
