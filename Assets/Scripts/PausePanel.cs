using UnityEngine;
using UnityEngine.Audio;
public class PausePanel : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";

    public AudioMixerGroup Mixer;


    //public AudioMixerSnapshot Normal;
    //public AudioMixerSnapshot InMenu;
    //variableAudioSource.pitch = Random.Range(0.9f, 1.1f); 
    private void OnEnable()
    {
        if (Mixer == null)
        {
            Mixer = GameObject.Find("MusicPlayer").GetComponent<AudioMixerGroup>();
        }
        Time.timeScale = 0;
        //InMenu.TransitionTo(0,5f);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat(MusicVolumeKey, Mathf.Lerp(-70, 5, volume));
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
        LVLChanger.Instance.SwitchScene((int)Scenes.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

