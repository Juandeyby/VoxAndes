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
