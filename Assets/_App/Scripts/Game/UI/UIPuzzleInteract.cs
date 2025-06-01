using UnityEngine;
using UnityEngine.UI;

public class UIPuzzleInteract : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;

    public void Config(Sprite sprite1, Sprite sprite2, Sprite sprite3)
    {
        image1.sprite = sprite1;
        image2.sprite = sprite2;
        image3.sprite = sprite3;
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