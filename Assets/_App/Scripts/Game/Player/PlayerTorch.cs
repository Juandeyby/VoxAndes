using System;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    [SerializeField] private Collider cTorch;
    
    private PlayerSound _playerSound;

    private void Awake()
    {
        DeactivateCollider();
        
        _playerSound = GetComponent<PlayerSound>();
    }

    private void ActivateCollider()
    {
        cTorch.enabled = true;
    }
    
    private void ActivateSound()
    {
        if (_playerSound != null)
        {
            _playerSound.PlayAttackSound();
        }
        else
        {
            Debug.LogWarning("PlayerSound component not found on the PlayerTorch GameObject.");
        }
    }
    
    private void DeactivateCollider()
    {
        cTorch.enabled = false;
    }
}
