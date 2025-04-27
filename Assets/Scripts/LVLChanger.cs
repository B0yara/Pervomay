using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public enum Scenes
{
    MainMenu = 0,
    Game = 1
}

public class LVLChanger : MonoBehaviour
{
    public static LVLChanger Instance => _instance;
    private static LVLChanger _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    public void SwitchScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
