using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sound FX Prefab")]
    [SerializeField] private AudioSource soundFXPrefab;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider UIVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;

    private string masterVolumeParameter = "MasterVolume";
    private string musicVolumeParameter = "MusicVolume";
    private string uiInteractionVolumeParameter = "UIVolume";
    private string soundFXVolumeParameter = "SoundFXVolume";

    //------------------//
    private void Awake()
    //------------------//
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

        masterVolumeSlider?.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider?.onValueChanged.AddListener(SetMusicVolume);
        UIVolumeSlider?.onValueChanged.AddListener(SetUIInteractionVolume);
        soundFXVolumeSlider?.onValueChanged.AddListener(SetSoundFXVolume);
    }

    //--------------------------------------------------------------------------------------//
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    //--------------------------------------------------------------------------------------//
    {
        // Spawn game object
        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform.position, Quaternion.identity);

        // Assign audio clip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play Sound
        audioSource.Play();

        // Get length of sound FX clip
        float clipLength = audioSource.clip.length;

        // Destroy clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }

    /*** Note: Decibels are logarithmic ***/

    //--------------------------------------//
    public void SetMasterVolume(float level)
    //--------------------------------------//
    {
        audioMixer.SetFloat(masterVolumeParameter, Mathf.Log10(level) * 20f);
    }

    //-------------------------------------//
    public void SetMusicVolume(float level)
    //-------------------------------------//
    {
        audioMixer.SetFloat(musicVolumeParameter, Mathf.Log10(level) * 20f);
    }

    //---------------------------------------------//
    public void SetUIInteractionVolume(float level)
    //---------------------------------------------//
    {
        audioMixer.SetFloat(uiInteractionVolumeParameter, Mathf.Log10(level) * 20f);
    }

    //---------------------------------------//
    public void SetSoundFXVolume(float level)
    //---------------------------------------//
    {
        audioMixer.SetFloat(soundFXVolumeParameter, Mathf.Log10(level) * 20f);
    }
}
