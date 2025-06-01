using System;
using UnityEngine;

public class MummyHand : MonoBehaviour
{
    [SerializeField] private Collider cHand;
    
    [SerializeField] private AudioClip handHitSound;
    private AudioManager _audioManager;

    private void Awake()
    {
        DeactivateCollider();   
        _audioManager = GameSingleton.Instance.AudioManager;
    }

    private void ActivateCollider()
    {
        _audioManager.PlaySFX(handHitSound);
        cHand.enabled = true;
    }
    
    private void DeactivateCollider()
    {
        cHand.enabled = false;
    }
}
