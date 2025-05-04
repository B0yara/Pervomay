using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    Slider _slider;
    public void Init(int maxHP)
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = maxHP;
        _slider.value = maxHP;
    }

    public void SetValue(int curHp)
    {
        _slider.value = curHp;
    }
}