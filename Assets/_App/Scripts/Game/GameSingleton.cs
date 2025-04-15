using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Instance { get; private set; }
    public GameSceneManager GameSceneManager { get; private set; }
    public UIGameManager UIGameManager { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        GameSceneManager = GetComponentInChildren<GameSceneManager>();
        UIGameManager = GetComponentInChildren<UIGameManager>();
    }
}
