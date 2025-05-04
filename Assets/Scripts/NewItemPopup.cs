using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewItemPopup : MonoBehaviour
{
    [SerializeField]
    TMP_Text ItemName;
    [SerializeField]
    Image Icon;
    [SerializeField]
    float ShowTime = 3f;
    public void ShowNewItem(string name)
    {
        ItemName.text = name;
        gameObject.SetActive(true);
        Invoke("Close", ShowTime);
    }
    void Close()
    {
        gameObject.SetActive(false);
    }
}
