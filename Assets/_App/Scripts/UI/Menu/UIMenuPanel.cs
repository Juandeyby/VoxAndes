using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuPanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        creditButton.onClick.AddListener(OnCreditButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnCreditButtonClicked()
    {
    }

    private void OnResumeButtonClicked()
    {
    }

    private void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("Player");
    }

    private void OnOptionsButtonClicked()
    {
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
