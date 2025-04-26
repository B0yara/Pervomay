using UnityEngine;
using UnityEngine.Audio;
public class PausePanel : MonoBehaviour
{
    public AudioMixerGroup Mixer;

    //public AudioMixerSnapshot Normal;
    //public AudioMixerSnapshot InMenu;

    private void OnEnable()
    {
        Time.timeScale = 0;
        //InMenu.TransitionTo(0,5f);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ChangeVolume(float volume) 
    { 
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-70, 5, volume));
        //PlayerPrefs.SetFloat("MusicVolume", volume);


    }

    //variableAudioSource.pitch = Random.Range(0.9f, 1.1f);


}

