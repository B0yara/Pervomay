using UnityEngine;

public class TempTutorChange : MonoBehaviour
{
    public GameObject objectToActivate; // Объект, который нужно включать/выключать
    public bool deactivateOnExit = true; // Отключать ли объект при выходе игрока

    private void OnTriggerEnter(Collider other)
    {
        // Если вошёл объект с тегом "Player"
        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);
            else
                Debug.LogWarning("No object to activate assigned!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Если вышел объект с тегом "Player" и включено deactivateOnExit
        if (other.CompareTag("Player") && deactivateOnExit)
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(false);
        }
    }
}