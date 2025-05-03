using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
    [SerializeField]
    GameObject DoorsColider;
    [SerializeField]
    GameObject LiftPanel;
    private int currentFloor = 2; // Текущий этаж лифта
    [SerializeField]
    private float speed = 2f; // Скорость лифта
    [SerializeField]
    private float targetFloorY; // Целевая позиция по Y для лифта

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter lift");
            DoorsColider.SetActive(true);
            LiftPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit lift");
            DoorsColider.SetActive(false);
            LiftPanel.SetActive(false);
        }
    }

    public void ChangeFloor(int floor)
    {
        Debug.Log("Change floor to: " + floor);
        if (floor == currentFloor) return; // Если этаж не изменился, выходим
        SceneManager.LoadScene($"{floor}lvl");
        // StartCoroutine(Go(floor));
        // Здесь можно добавить логику для перемещения лифта на нужный этаж
    }

    IEnumerator Go(int floor)
    {
        Debug.Log("Go to floor: " + floor);
        float newTrget = floor > currentFloor ? targetFloorY : -targetFloorY;
        while (Mathf.Abs(transform.position.y - targetFloorY) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetFloorY, transform.position.z), speed * Time.deltaTime);
            yield return null;
        }
        currentFloor = floor;
    }
}
