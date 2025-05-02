using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Transform target;
    private LayerMask targetLayer;
    private float speed = 10f;

    public void Initialize(int damage, Transform target, LayerMask targetLayer)
    {
        this.damage = damage;
        this.target = target;
        this.targetLayer = targetLayer;
        Destroy(gameObject, 5f); // Автоуничтожение через 5 сек
    }

    private void Update()
    {
        if (target != null)
        {
            // Летим прямо вперед (не преследуем цель)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            if (other.transform == target)
            {
                if (target.TryGetComponent<_CanDamage>(out var damageable))
                {
                    damageable.GetDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}