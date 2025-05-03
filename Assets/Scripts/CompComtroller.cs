using System.Collections;
using UnityEngine;

public class CompComtroller : MonoBehaviour
{

    [SerializeField]
    GameObject CompHint;
    [SerializeField]
    GameObject PCPanel;
    [SerializeField]
    MeshRenderer _meshRenderer;

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
            StartCoroutine(Desolve());
        }
        else
        {
            Debug.Log("Incorrect password!"); // Здесь можно добавить логику для обработки неправильного пароля
        }
    }

    IEnumerator Desolve()
    {
        for (var t = 0f; t < 5f; t += Time.deltaTime)
        {
            _meshRenderer.material.SetFloat("_Dissolve", t / 10f);
            yield return null;
        }
    }
}
