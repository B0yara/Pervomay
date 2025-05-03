using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    int Enemies = 10;
    [SerializeField]
    Transform door;
    [SerializeField]
    float doorOpenY = 90f;
    [SerializeField]
    float openningDuration = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyController.Instance.OnEnemiesDead = EnemyKilled;
    }

    public void EnemyKilled()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        var beginPos = door.localPosition;
        var endPos = new Vector3(door.localPosition.x, doorOpenY, door.localPosition.z);
        for (float t = 0; t < openningDuration; t += Time.deltaTime)
        {
            door.localPosition = Vector3.Lerp(beginPos, endPos, t / openningDuration);
            yield return null;
        }
    }
}
