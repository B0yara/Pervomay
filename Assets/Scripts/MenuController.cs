using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
        GameController.Instance.SwitchPauseButton(false);
    }
    public void Play()
    {
        GameController.Instance.SwitchPauseButton(true);
        SceneManager.LoadScene("TempCutScene");
    }
}
