using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneOnCutTimer : MonoBehaviour
{
    public float ChangeTime;
    public string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTime -= Time.deltaTime;
        if (ChangeTime <= 0) { SceneManager.LoadScene(SceneName); }
    }
}
