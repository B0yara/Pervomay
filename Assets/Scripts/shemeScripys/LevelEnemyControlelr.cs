using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; private set; }

    [Header("Group Behavior")]
    public float groupUpdateInterval = 0.3f;
    public bool noEnemies = false;

    public List<EntityEnemy> activeEnemies = new List<EntityEnemy>();
    public List<GameObject> spawns = new List<GameObject>();
    private float lastUpdateTime;
    public Action OnEnemiesDead;

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
        // �����������: ��������� ���������� � ����������
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

    public void RegisterSpawns(GameObject spawn)
    {
        if (!spawns.Contains(spawn))
        {
            spawns.Add(spawn);
        }
    }

    public void UnregisterSpawns(GameObject spawn)
    {
        if (spawns.Contains(spawn))
        {
            spawns.Remove(spawn);
        }
    }

    public void UnregisterEnemy(EntityEnemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            noEnemies = false;
            activeEnemies.Remove(enemy);
        }
        if (activeEnemies.Count == 0)
        {
            noEnemies = true;
            OnEnemiesDead?.Invoke();
        }
    }

}