using TMPro;
using UnityEngine;

public class BabkaController : MonoBehaviour
{

    private const string OPEN_DIALOG_HINT = "Нжмите 'E'";
    private const string NEXT_DIALOG_HINT = "Далее 'E'";
    private const string CLOSE_DIALOG_HINT = "Закрыть 'E'";

    public static readonly Item EmptyUsb = new Item { itemName = "EmptyUSB", itemID = 0 };
    public static readonly Item VirusUsb = new Item { itemName = "VirusUSB", itemID = 4 };

    [SerializeField]
    TMP_Text BabkaText;
    [SerializeField]
    private string[] _texts;

    [SerializeField]
    TMP_Text DialogHint;

    [SerializeField]
    GameObject DialogPanel;


    private bool _playerInTrigger = false;
    private int _currentTextIndex = 0;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Babka");
        var inv = GameController.Instance.Inventory;
        if (inv.Contains(VirusUsb))
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            DialogHint.gameObject.SetActive(true);
            DialogHint.text = OPEN_DIALOG_HINT;
            _playerInTrigger = true;
        }
    }

    void StartDialog()
    {
        DialogPanel.SetActive(true);
        BabkaText.text = _texts[_currentTextIndex];
        DialogHint.text = NEXT_DIALOG_HINT;
    }


    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit Babka");
        if (other.CompareTag("Player"))
        {
            DialogHint.gameObject.SetActive(false);
            _currentTextIndex = 0;
            _playerInTrigger = false;
            DialogPanel.SetActive(false);
        }
    }

    void Interact()
    {
        var inv = GameController.Instance.Inventory;
        if (inv.Contains(VirusUsb) || GameController.Instance.VirusLoaded)
        {
            return;
        }
        if (inv.Contains(CompComtroller.BackupUsb))
        {
            inv.AddItem(VirusUsb);
            inv.RemoveItem(CompComtroller.BackupUsb);
            EndDialog();
            DialogHint.gameObject.SetActive(false);
        }
        else
        {
            StartDialog();
        }

    }

    void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            Debug.Log("E pressed");
            if (DialogPanel.activeSelf)
            {
                NextDialog();
            }
            else
            {
                Interact();
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
            EndDialog();

            DialogHint.text = OPEN_DIALOG_HINT;
        }
        BabkaText.text = _texts[_currentTextIndex];
    }

    void EndDialog()
    {
        _currentTextIndex = 0;
        DialogPanel.SetActive(false);
    }

    void GiveUSB()
    {
        GameController.Instance.Inventory.AddItem(EmptyUsb);
    }
}
