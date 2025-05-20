using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementController playerMovementController;
    public PlayerMovementController PlayerMovementController => playerMovementController;
    [SerializeField] private PlayerHealth playerHealth;
    public PlayerHealth PlayerHealth => playerHealth;

    public void ResetPlayer()
    {
        StartCoroutine(ResetLevel());
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(4f);
        
        var currentScene = SceneManager.GetActiveScene().name;
        GameSingleton.Instance.GameSceneManager.ChangeLevel(currentScene);
        
        playerHealth.Init();
        playerMovementController.Init();
    }
}
