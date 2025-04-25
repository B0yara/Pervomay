using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void SetVolume(float vol) 
    {
        musicVolume = vol;
    }
}
