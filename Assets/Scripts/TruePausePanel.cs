using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class TruePausePanel : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";

    private const string VFXVolumeKey = "VFXVolume";

    public AudioMixerGroup Mixer;


    
    
    //variableAudioSource.pitch = Random.Range(0.9f, 1.1f); 
    private void OnEnable()
    {
        if (Mixer == null)
        {
            Mixer = GameObject.Find("MusicPlayer").GetComponent<AudioMixerGroup>();
        }
        Time.timeScale = 0;
        //public AudioMixerSnapshot InMenu;
        //InMenu.TransitionTo(0,5f);
    }

    private void OnDisable()
    {
        //public AudioMixerSnapshot Normal;
        Time.timeScale = 1;

    }

    public void ChangeVolumeMusic(float volume)
    {
        Mixer.audioMixer.SetFloat(MusicVolumeKey, Mathf.Lerp(-70, 0, volume));
        //PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void ChangeVolumeVFX(float volume)
    {
        Mixer.audioMixer.SetFloat(VFXVolumeKey, Mathf.Lerp(-70, 0, volume));
        //PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SwichWisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        //LVLChanger.Instance.SwitchScene((int)Scenes.MainMenu);
        GameController.Instance.MainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

