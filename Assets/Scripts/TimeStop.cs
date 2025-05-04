using UnityEngine;

public class TimeStop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
