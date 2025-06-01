using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UIPauseMainPanel pauseMainPanel;
    public UIPauseMainPanel PauseMainPanel => pauseMainPanel;
    [SerializeField] private UIPauseSettingsPanel pauseSettingsPanel;
    public UIPauseSettingsPanel PauseSettingsPanel => pauseSettingsPanel;
    
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