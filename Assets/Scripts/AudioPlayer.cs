using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;


/*// Get reference to your AudioPlayer
AudioPlayer audioPlayer = GetComponent<AudioPlayer>();

// Play a sound
audioPlayer.Play("SoundName");

// Stop a sound
audioPlayer.Stop("SoundName");

// Adjust volume of a specific sound
audioPlayer.SetVolume("SoundName", 0.5f);

// Adjust mixer group volume (must match exposed parameter name)
audioPlayer.SetMixerVolume("MusicVolume", 0.7f);

*/


[System.Serializable]
public class AudioElement
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop = false;
    public bool playOnAwake = false;

    [HideInInspector] public AudioSource source;
}

public class AudioPlayer : MonoBehaviour
{
    public AudioMixer masterMixer;
    public List<AudioElement> audioElements = new List<AudioElement>();

    private Dictionary<string, AudioElement> audioDictionary = new Dictionary<string, AudioElement>();

    private void Awake()
    {
        // Initialize all audio sources
        foreach (var element in audioElements)
        {
            GameObject child = new GameObject(element.name + " AudioSource");
            child.transform.SetParent(transform);

            element.source = child.AddComponent<AudioSource>();
            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.pitch = element.pitch;
            element.source.loop = element.loop;
            element.source.playOnAwake = element.playOnAwake;
            element.source.outputAudioMixerGroup = element.mixerGroup;

            if (element.playOnAwake)
            {
                element.source.Play();
            }

            audioDictionary.Add(element.name, element);
        }
    }

    // Play audio by name
    public void Play(string name)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            element.source.Play();
        }
        else
        {
            Debug.LogWarning("Audio element not found: " + name);
        }
    }

    // Stop audio by name
    public void Stop(string name)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            element.source.Stop();
        }
        else
        {
            Debug.LogWarning("Audio element not found: " + name);
        }
    }

    // Pause audio by name
    public void Pause(string name)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            element.source.Pause();
        }
        else
        {
            Debug.LogWarning("Audio element not found: " + name);
        }
    }

    // Set volume for specific audio element
    public void SetVolume(string name, float volume)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            element.source.volume = Mathf.Clamp01(volume);
        }
        else
        {
            Debug.LogWarning("Audio element not found: " + name);
        }
    }

    // Set pitch for specific audio element
    public void SetPitch(string name, float pitch)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            element.source.pitch = Mathf.Clamp(pitch, 0.1f, 3f);
        }
        else
        {
            Debug.LogWarning("Audio element not found: " + name);
        }
    }

    // Check if audio is playing
    public bool IsPlaying(string name)
    {
        if (audioDictionary.TryGetValue(name, out AudioElement element))
        {
            return element.source.isPlaying;
        }

        Debug.LogWarning("Audio element not found: " + name);
        return false;
    }

    // Set mixer group volume by exposed parameter name
    public void SetMixerVolume(string exposedParam, float volume)
    {
        if (masterMixer != null)
        {
            // Convert linear 0-1 volume to dB scale
            float dB = volume <= 0 ? -80f : Mathf.Log10(volume) * 20f;
            masterMixer.SetFloat(exposedParam, dB);
        }
        else
        {
            Debug.LogWarning("Master mixer not assigned");
        }
    }
}