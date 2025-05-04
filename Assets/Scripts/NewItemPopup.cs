using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewItemPopup : MonoBehaviour
{
    private const string PATH_MASK = "Icons/{0}.png";
    [SerializeField]
    TMP_Text ItemName;
    [SerializeField]
    Image Icon;
    [SerializeField]
    float ShowTime = 3f;
    public void ShowNewItem(string name)
    {
        ItemName.text = name;
        Icon.sprite = Resources.Load(string.Format(PATH_MASK, name)) as Sprite;
        gameObject.SetActive(true);
        Invoke("Close", ShowTime);
    }
    void Close()
    {
        gameObject.SetActive(false);
    }
}
