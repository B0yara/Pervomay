using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField] private int stage = 0;
    [SerializeField] bool stageEnd = false;


    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private Transform SpawnPoint;

    protected override void Start()
    {
        base.Start();
        StartStage(1);
        FindTarget();

    }

    protected virtual void FixedUpdate()
    {
        if (EnemyController.Instance.spawns.Count <= 0)
        {
            if (EnemyController.Instance.activeEnemies.Count <= 0)
            {
                if (++stage > enemies.Count) stage = -1;

                if (stageEnd)
                {
                    StartStage(++stage);
                }
            }
        }
    }
    protected override void Die(float time)
    {
        Debug.Log("[Boss] Пытаемся сдохнуть");
        if (GameController.Instance != null)
        {
            Debug.Log("[Boss] GameController !null");

            if (GameController.Instance.VirusIsLoaded())
            {
                Debug.Log("[Boss] Вирус есть");
                animator.SetBool("EndDeath", true);
                GameObject.Find("EndCanvas").SetActive(true);
                GameController.Instance.BossIsDeath();
                GetComponent<Collider>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
                base.Die(time);
            }
            else
            {
                Debug.Log("[Boss] Вируса нет");
                animator.SetBool("Death", true);
                animator.SetBool("EndDeath", false);
                GetComponent<Collider>().enabled = false;
                GetComponent<CharacterController>().enabled = false;
            }
        }

    }

    void TryToDie()
    {

    }
    protected virtual void BossLife()
    {
        stage = 0;
        StartStage(stage);
    }
    protected virtual void StartStage(int stage)
    {
        stageEnd = false;
        this.stage = stage;
        if (stage > 3) stage = 1;
        switch (stage)
        {
            case 1:
                // ���� 1: ������ ������ ������ ������
                SummonEnemyGroup(1);
                Debug.Log("���� 1: ������� ������ ������ 1");
                break;

            case 2:
                // ���� 2: ������ ������ ������ ������
                SummonEnemyGroup(2);
                Debug.Log("���� 2: ������� ������ ������ 2");
                break;

            case 3:
                // ���� 3: ������ ������� ������ ������
                SummonEnemyGroup(3);
                Debug.Log("���� 3: ������� ������ ������ 3");
                break;

            default:
                Debug.LogError($"����������� ����: {stage}");
                break;
        }

    }
    protected override void HandleMovement()
    {
        // Quaternion targetRotation = Quaternion.LookRotation(currentTarget.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    protected override void HandleGravity()
    {

    }
    private void SummonEnemyGroup(int groupID)
    {
        Instantiate(enemies[groupID], SpawnPoint);

    }

}
