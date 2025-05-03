using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoneData : MonoBehaviour
{
    int damage;
    LayerMask targetLayer;
    public void Initialize(int damage, LayerMask layer)
    {
        this.damage = damage;
        this.targetLayer = layer;
    }
    public void DestroyZone()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            Transform target = other.transform;
            if (target.TryGetComponent<_CanDamage>(out var damageble))
            {
                damageble.GetDamage(damage);
            }
        }
    }

}