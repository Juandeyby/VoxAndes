using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;
    
    private string _currentLevel;
    public bool IsGamePaused { get; set; }

    private void Awake()
    {
        pauseAction.action.performed += OnPauseAction;
    }

    private void OnPauseAction(InputAction.CallbackContext obj)
    {
        if (IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Start()
    {
        var levelNumber = GameSingleton.Instance.DataManager.GameData.CurrentLevel;
        StartCoroutine(LoadScene($"Level{levelNumber}"));
    }

    public IEnumerator NextLevel()
    {
        var dataManager = GameSingleton.Instance.DataManager;
        var levelNumber = dataManager.GameData.CurrentLevel;
        var nextLevelIndex = levelNumber + 1;
        dataManager.SaveLevel(nextLevelIndex);
        dataManager.SaveData();
        
        var player = GameSingleton.Instance.PlayerManager.Player;
        player.ResetValues();

        yield return null;
        
        ChangeLevel($"Level{nextLevelIndex}");
    }

    public void ChangeLevel(string newLevel)
    {
        StartCoroutine(SwitchLevel(newLevel));
    }

    private IEnumerator LoadScene(string newLevel)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(newLevel, LoadSceneMode.Additive);
        while (asyncLoad is { isDone: false })
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newLevel));
        _currentLevel = newLevel;
    }
    
    private IEnumerator SwitchLevel(string newLevel)
    {
        if (!string.IsNullOrEmpty(_currentLevel))
        {
            var asyncUnload = SceneManager.UnloadSceneAsync(_currentLevel);
            while (asyncUnload is { isDone: false })
                yield return null;
        }

        yield return LoadScene(newLevel);
    }

    public void ResumeGame()
    {
        var uiPauseMenu = GameSingleton.Instance.UIGameManager.PauseMenu;
        uiPauseMenu.Hide();
        
        IsGamePaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        var uiPauseMenu = GameSingleton.Instance.UIGameManager.PauseMenu;
        uiPauseMenu.Show();
        
        IsGamePaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
