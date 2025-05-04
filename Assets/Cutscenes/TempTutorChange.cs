using UnityEngine;

public class TempTutorChange : MonoBehaviour
{
    public GameObject objectToActivate; // ������, ������� ����� ��������/���������
    public bool deactivateOnExit = true; // ��������� �� ������ ��� ������ ������

    private void OnTriggerEnter(Collider other)
    {
        // ���� ����� ������ � ����� "Player"
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
        // ���� ����� ������ � ����� "Player" � �������� deactivateOnExit
        if (other.CompareTag("Player") && deactivateOnExit)
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(false);
        }
    }
}