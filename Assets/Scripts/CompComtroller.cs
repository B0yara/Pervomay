using UnityEngine;

public class CompComtroller : MonoBehaviour
{

    [SerializeField]
    GameObject CompHint;
    [SerializeField]
    GameObject PCPanel;

    private bool _playerInTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter comp");
        if (other.CompareTag("Player"))
        {

            CompHint.SetActive(true);
            _playerInTrigger = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit comp");
        if (other.CompareTag("Player"))
        {

            CompHint.SetActive(false);
            _playerInTrigger = false;
        }
    }

    void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            CompHint.SetActive(false);
            PCPanel.SetActive(true); // Открываем панель компьютера
            // Здесь можно добавить логику для открытия интерфейса компьютера или выполнения другого действия
        }
    }

    public void OnEndEdit(string password)
    {
        if (password == "1234")
        {
            Debug.Log("Correct password!"); // Здесь можно добавить логику для открытия двери или выполнения другого действия
        }
        else
        {
            Debug.Log("Incorrect password!"); // Здесь можно добавить логику для обработки неправильного пароля
        }
    }
}
