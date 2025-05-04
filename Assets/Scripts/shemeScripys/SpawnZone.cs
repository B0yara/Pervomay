using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    [SerializeField] private ParticleSystem _spawnSystem;
    [SerializeField] private Animator spawnerAnimator;
    public GameObject entity;
    private GameObject spawnEntity;

    /// <summary>
    /// Âûחûגאועסÿ ג animator
    /// </summary>
    public void CreateEnemy()
    {
        spawnEntity = Instantiate(entity, transform);
        spawnEntity.transform.parent = null;
    }
    public void StartParticles()
    {
        _spawnSystem.Play();
    }

    public void DestroyThis()
    {
        EnemyController.Instance.UnregisterSpawns(gameObject);
        Destroy(gameObject);
    }
    public void Start()
    {
        EnemyController.Instance.RegisterSpawns(gameObject);
    }
}
