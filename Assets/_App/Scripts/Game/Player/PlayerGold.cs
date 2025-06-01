using System;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    private UIGold _uiPlayerGold;
    [SerializeField] private AudioClip coinSound;
    private int _gold;

    private void Start()
    {
        _uiPlayerGold = GameSingleton.Instance.UIGameManager.HubMenu.UIGold;
    }

    public void AddGold(int i)
    {
        _gold += i;
        _uiPlayerGold.SetGold(_gold);
        
        if (coinSound != null)
        {
            GameSingleton.Instance.AudioManager.PlaySFX(coinSound);
        }
        else
        {
            Debug.LogWarning("Coin sound not assigned.");
        }
    }
}
