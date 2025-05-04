using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CompComtroller : MonoBehaviour
{
    [SerializeField]
    Item[] RequireItems;
    [SerializeField]
    GameObject CompHint;
    [SerializeField]
    GameObject PCPanel;

    [SerializeField]
    InventoryItem[] DraggableItems;
    [SerializeField]
    GameObject BackupButton;

    [SerializeField]
    TMP_Text PinHint;

    private bool _playerInTrigger = false;
    private bool _pinSuccess = false;
    public static Item BackupUsb = new Item { itemName = "BackupUSB", itemID = 3 };

    List<Item> _items = new List<Item>();
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
            PCPanel.SetActive(false);
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
            PCPanel.SetActive(true);
            Invoke("ForceUpdate", 0.01f);
            foreach (var item in DraggableItems)
            {
                item.Init(this);
            }
        }
    }
    void ForeUpdate()
    {
        PCPanel.GetComponent<RectTransform>().ForceUpdateRectTransforms();
    }
    public void OnEndEdit(string password)
    {
        if (password == "1234")
        {
            Debug.Log("Correct password!"); // Здесь можно добавить логику для открытия двери или выполнения другого действия
            _pinSuccess = true;
            AllItemsReady();
        }
        else
        {
            _pinSuccess = false;
            Debug.Log("Incorrect password!"); // Здесь можно добавить логику для обработки неправильного пароля
        }
    }

    public void GiveUSBBackUp()
    {
        var inv = GameController.Instance.Inventory;
        foreach (var item in _items)
        {
            inv.RemoveItem(item);
        }
        inv.AddItem(BackupUsb);
        PCPanel.SetActive(false);
        BackupButton.SetActive(false);

    }

    internal void PlaceItem(Item item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
        AllItemsReady();
    }

    private void AllItemsReady()
    {
        foreach (var item in RequireItems)
        {
            if (!_items.Contains(item))
            {
                PinHint.text = "Необходимо вставить все устройства";
                return;
            }
        }
        if (!_pinSuccess)
        {
            PinHint.text = "Введите пин";
        }

        BackupButton.SetActive(_pinSuccess);
    }
}
