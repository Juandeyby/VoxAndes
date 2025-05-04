using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseSettingsPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    [SerializeField] private Button backButton;

    private void Awake()
    {
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        
        backButton.onClick.AddListener(OnBackButtonClicked);
    }
    
    private void Start()
    {
        var audioManager = GameSingleton.Instance.AudioManager;
        
        var masterVolume = audioManager.GetMasterVolume();
        masterVolumeSlider.SetValueWithoutNotify(masterVolume);
        
        var musicVolume = audioManager.GetMusicVolume();
        musicVolumeSlider.SetValueWithoutNotify(musicVolume);
        
        var sfxVolume = audioManager.GetSfxVolume();
        sfxVolumeSlider.SetValueWithoutNotify(sfxVolume);
    }

    private void OnMusicVolumeChanged(float arg0)
    {
        var audioManager = GameSingleton.Instance.AudioManager;
        var audioMixer = audioManager.Mixer;
        audioMixer.SetFloat("Music", Mathf.Log10(arg0) * 20);
        
        audioManager.SaveMusicVolume(arg0);
    }

    private void OnSfxVolumeChanged(float arg0)
    {
        var audioManager = GameSingleton.Instance.AudioManager;
        var audioMixer = audioManager.Mixer;
        audioMixer.SetFloat("SFX", Mathf.Log10(arg0) * 20);
        
        audioManager.SaveSfxVolume(arg0);
    }

    private void OnMasterVolumeChanged(float arg0)
    {
        var audioManager = GameSingleton.Instance.AudioManager;
        var audioMixer = audioManager.Mixer;
        audioMixer.SetFloat("Master", Mathf.Log10(arg0) * 20);
        
        audioManager.SaveMasterVolume(arg0);
    }

    private void OnBackButtonClicked()
    {
        var uiPauseMenu = GameSingleton.Instance.UIGameManager.PauseMenu;
        uiPauseMenu.PauseMainPanel.Show();
        uiPauseMenu.PauseSettingsPanel.Hide();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
