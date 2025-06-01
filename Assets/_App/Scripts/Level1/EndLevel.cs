using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Collider collider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var currentLevel = SceneManager.GetActiveScene().name;
            if (currentLevel == "Level2")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                SceneManager.LoadScene("Menu");
                return;
            }
            
            var gameSceneManager = GameSingleton.Instance.GameSceneManager;
            if (gameSceneManager != null)
            {
                collider.enabled = false; // Disable the collider to prevent multiple triggers
                StartCoroutine(gameSceneManager.NextLevel());
            }
            else
            {
                Debug.LogError("GameSceneManager is not initialized.");
            }
        }
    }
}
