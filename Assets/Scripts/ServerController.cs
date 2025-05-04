using TMPro;
using UnityEngine;

public class ServerController : MonoBehaviour
{
    private const string LOAD_VIRUS = "Загрузить вирус 'E'";
    private const string NEED_VIRUS = "Нужен вирус";
    private const string VIRUS_LOADED = "ВИРУС ЗАГРУЖЕН!";
    private const string TO_BABKA = "ХЗ что это такое";

    [SerializeField]
    TMP_Text DialogHint;

    private bool _playerInTrigger = false;
    private bool _haveVirus = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Server");

        var inv = GameController.Instance.Inventory;

        if (other.CompareTag("Player"))
        {
            DialogHint.text = TO_BABKA;
            if (inv.Contains(BabkaController.EmptyUsb) || inv.Contains(CompComtroller.BackupUsb))
            {
                DialogHint.text = NEED_VIRUS;
            }
            if (inv.Contains(BabkaController.VirusUsb))
            {
                DialogHint.text = LOAD_VIRUS;
                _haveVirus = true;
            }
            if (GameController.Instance.VirusLoaded)
            {
                DialogHint.text = VIRUS_LOADED;
            }
            DialogHint.gameObject.SetActive(true);
            _playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit Server");
        if (other.CompareTag("Player"))
        {
            DialogHint.gameObject.SetActive(false);
            _playerInTrigger = false;
            _haveVirus = true;
        }
    }

    void Update()
    {
        if (_playerInTrigger && Input.GetKeyDown(KeyCode.E) && _haveVirus)
        {
            GameController.Instance.LoadVirus();
            DialogHint.text = VIRUS_LOADED;
            GameController.Instance.Inventory.RemoveItem(BabkaController.VirusUsb);
        }
    }

}
