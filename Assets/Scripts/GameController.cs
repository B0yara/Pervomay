using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverPanel;
    [SerializeField]
    GameObject PauseButton;
    [SerializeField]
    NewItemPopup NewItemPopup;
    public NewItemPopup newItemPopup => NewItemPopup;
    static GameController _instance;
    public bool bossIsDead = false;
    public static GameController Instance => _instance;

    public Inventory Inventory;

    private bool _virusLoaded = false;
    public bool VirusLoaded => _virusLoaded;

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
        Inventory = new Inventory(this);
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
        Inventory.Clear();
        _virusLoaded = false;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    internal void LoadVirus()
    {
        _virusLoaded = true;
    }
    public bool VirusIsLoaded()
    {
        return _virusLoaded;
    }
    public void BossIsDeath()
    {
        if (_virusLoaded)
        {
            bossIsDead = true;
            List<EntityEnemy> enemies = EnemyController.Instance.activeEnemies;
            foreach (var entity in enemies)
            {
                EnemyController.Instance.UnregisterEnemy(entity);
                Destroy(entity.gameObject);
            }
        }
    }

    internal void SwitchPauseButton(bool v)
    {
        PauseButton.SetActive(v);
    }
}
