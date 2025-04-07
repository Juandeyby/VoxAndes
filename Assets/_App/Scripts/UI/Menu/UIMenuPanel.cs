using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuPanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnResumeButtonClicked()
    {
    }

    private void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OnOptionsButtonClicked()
    {
    }

    private void OnQuitButtonClicked()
    {
    }
}
