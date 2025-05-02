using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }

    [Header("Group Behavior")]
    public float groupUpdateInterval = 0.3f;

    private List<EntityEnemy> activeEnemies = new List<EntityEnemy>();
    private float lastUpdateTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // Оптимизация: групповое обновление с интервалом
        if (Time.time - lastUpdateTime > groupUpdateInterval)
        {
            lastUpdateTime = Time.time;
        }
    }

    public void RegisterEnemy(EntityEnemy enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(EntityEnemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }

}