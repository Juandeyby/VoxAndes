using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMainPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnResumeButtonClicked()
    {
        GameSingleton.Instance.GameSceneManager.ResumeGame();
    }
    
    private void OnSettingsButtonClicked()
    {
        var uiPauseMenu = GameSingleton.Instance.UIGameManager.PauseMenu;
        uiPauseMenu.PauseMainPanel.Hide();
        uiPauseMenu.PauseSettingsPanel.Show();
    }
    
    private void OnQuitButtonClicked()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("Menu");
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
