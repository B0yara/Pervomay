using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverPanel;

    static GameController _instance;
    public static GameController Instance => _instance;

    public Inventory Inventory;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    void Init()
    {
        Inventory = new Inventory();
        MainMenu();
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
