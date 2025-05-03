using TMPro;
using UnityEngine;

public class BabkaController : MonoBehaviour
{
    
    private const string OPEN_DIALOG_HINT = "Нжмите 'E'";
    private const string NEXT_DIALOG_HINT = "Далее 'E'";
    private const string CLOSE_DIALOG_HINT = "Закрыть 'E'";

    private static Item EmptyUsb = new Item { itemName = "EmptyUSB", itemID = 0 };

    [SerializeField]
    TMP_Text BabkaText;
    [SerializeField]
    private string[] _texts;

    [SerializeField]
    TMP_Text DialogHint;


    private bool _playerInTrigger = false;
    private int _currentTextIndex = 0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter comp");
        if (other.CompareTag("Player"))
        {
            DialogHint.gameObject.SetActive(true);
            DialogHint.text = OPEN_DIALOG_HINT;
            _playerInTrigger = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit comp");
        if (other.CompareTag("Player"))
        {
            DialogHint.gameObject.SetActive(false);
            _currentTextIndex = 0;
            _playerInTrigger = false;
            BabkaText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (BabkaText.gameObject.activeSelf)
            {
                NextDialog();
            }
            else
            {
                BabkaText.gameObject.SetActive(true);
                BabkaText.text = _texts[_currentTextIndex];
                DialogHint.text = NEXT_DIALOG_HINT;
            }
        }
    }

    void NextDialog()
    {
        _currentTextIndex++;

        if (_currentTextIndex == _texts.Length - 1)
        {
            DialogHint.text = CLOSE_DIALOG_HINT;
            GiveUSB();
        }

        if (_currentTextIndex >= _texts.Length)
        {
            _currentTextIndex = 0;
            BabkaText.gameObject.SetActive(false);
            DialogHint.text = OPEN_DIALOG_HINT;
        }
        BabkaText.text = _texts[_currentTextIndex];
    }

    void GiveUSB()
    {
        GameController.Instance.Inventory.AddItem(EmptyUsb);
    }
}
