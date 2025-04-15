using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Level_1";
    private string _currentLevel;
    
    private void Start()
    {
        StartCoroutine(LoadScene(sceneToLoad));
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
}
