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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyKilled()
    {
        Enemies--;
        if (Enemies <= 0)
        {
            // Здесь можно добавить логику для завершения уровня или перехода к следующему
            Debug.Log("All enemies killed! Level complete!");
            StartCoroutine(OpenDoor());
        }
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
