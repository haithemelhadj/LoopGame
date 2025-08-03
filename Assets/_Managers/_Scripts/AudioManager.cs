using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip winAudioClip;
    public AudioSource audioSource;


    public void PlayAudioClipWithIndex(int index)
    {
        if (index < 0 || index >= audioClips.Length)
        {
            Debug.LogError("Invalid audio clip index: " + index);
            return;
        }
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void PlayWinAudioClip()
    {
        if (winAudioClip == null)
        {
            Debug.LogError("Win audio clip is not assigned.");
            return;
        }
        audioSource.clip = winAudioClip;
        audioSource.Play();
    }
    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void MuteAudio()
    {
        audioSource.mute = !audioSource.mute;
    }

    public float volume = 1f;
    public float pitch = 1;
    public void SoundSettings()
    {

    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = volume;
    }
    public void SetPitch(float newPitch)
    {
        float rand = Random.Range(-0.3f, 0.4f);
        newPitch += rand; // Add a random value to the pitch
        //newPitch = Mathf.Clamp(newPitch, 0, 3); // Ensure pitch is within a valid range (0 to 3)
        pitch = newPitch;
        audioSource.pitch = pitch;
    }

}
