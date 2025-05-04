using System;

using Unity.Mathematics;

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    GameObject BlooshPrefab;
    //private AudioSource audioSource;
    [SerializeField] ParticleSystem BlastEffect;


    private int damage;
    private Transform target;
    private LayerMask targetLayer;
    private float speed = 10f;

    public void Initialize(int damage, Transform target, LayerMask targetLayer)
    {
        this.damage = damage;
        this.target = target;
        this.targetLayer = targetLayer;
        Destroy(gameObject, 5f); // ��������������� ����� 5 ���
    }

    private void Update()
    {
        if (target != null)
        {
            // ����� ����� ������ (�� ���������� ����)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Im Find Collider");
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            if (other.transform == target)
            {
                if (target.TryGetComponent<_CanDamage>(out var damageable))
                {
                    damageable.GetDamage(damage);
                }
            }
            Instantiate(BlooshPrefab, other.transform.position, Quaternion.identity, other.gameObject.transform);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Im Find Collision");
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            if (other.transform == target)
            {
                if (target.TryGetComponent<_CanDamage>(out var damageable))
                {
                    damageable.GetDamage(damage);
                }
            }
            Instantiate(BlooshPrefab, other.contacts[0].point, Quaternion.Euler(other.contacts[0].normal * -180), other.gameObject.transform);
            Destroy(gameObject);
        }
    }
}

