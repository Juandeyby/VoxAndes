using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip footstepClip;
    
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;
    public AudioMixer Mixer => mixer;

    private void Start()
    {
        musicSource.clip = musicClips[0];
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip, float volume = 0.5f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
    
    public void SaveMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat("Music", musicVolume);
    }
    
    public void SaveSfxVolume(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SFX", sfxVolume);
    }

    public void SaveMasterVolume(float masterVolume)
    {
        PlayerPrefs.SetFloat("Master", masterVolume);
    }


    public float GetMasterVolume()
    {
        var masterVolume = PlayerPrefs.GetFloat("Master", 0);
        mixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        return masterVolume;
    }
    
    public float GetMusicVolume()
    {
        var musicVolume = PlayerPrefs.GetFloat("Music", 0);
        mixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        return musicVolume;
    }
    
    public float GetSfxVolume()
    {
        var sfxVolume = PlayerPrefs.GetFloat("SFX", 0);
        mixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        return sfxVolume;
    }
}
