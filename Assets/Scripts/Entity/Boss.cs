using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField] private int stage = -1;
    [SerializeField] bool stageEnd = false;


    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private Transform SpawnPoint;
    
    protected override void Start()
    {
        base.Start();
        BossStage(0);
        FindTarget();

    }
    protected override void Die(float time)
    {
        if (GameController.Instance != null)
        {
            if (GameController.Instance.VirusIsLoaded())
            {
                animator.SetBool("EndDeath", true);
                GameObject.Find("EndCanvas").SetActive(true);
                GameController.Instance.BossIsDeath();
                base.Die(time);
            }
            else
            {
                animator.SetBool("Death", true);
                animator.SetBool("EndDeath", false);
                GetComponent<Collider>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
                BossLife();
            }
        }

    }

    protected virtual void BossLife()
    {
        BossLife();
    }
    protected virtual void BossStage(int stage)
    {
        if (this.stage != stage || !GameController.Instance.VirusIsLoaded())
        {
            stageEnd = false;
            StartStage(stage);
        }
        else
        {
            stageEnd = true;
        }
        this.stage = stage;
    }
    protected virtual void StartStage(int stage)
    {
        stageEnd = true;

        switch (stage)
        {
            case 1:
                // Этап 1: Призыв первой группы врагов
                SummonEnemyGroup(1);
                Debug.Log("Этап 1: Вызвана группа врагов 1");
                break;

            case 2:
                // Этап 2: Призыв второй группы врагов
                SummonEnemyGroup(2);
                Debug.Log("Этап 2: Вызвана группа врагов 2");
                break;

            case 3:
                // Этап 3: Призыв третьей группы врагов
                SummonEnemyGroup(3);
                Debug.Log("Этап 3: Вызвана группа врагов 3");
                break;

            default:
                Debug.LogError($"Неизвестный этап: {stage}");
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    private void SummonEnemyGroup(int groupID)
    {
        Instantiate(enemies[groupID], SpawnPoint);
    }

}
