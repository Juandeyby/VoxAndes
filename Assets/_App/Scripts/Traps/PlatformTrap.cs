using System;
using UnityEngine;

public class PlatformTrap : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive = true;
    
    [SerializeField] private AudioClip trapSound;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameSingleton.Instance.AudioManager;
    }

    private void PlayTrapSound()
    {
        if (trapSound != null)
        {
            _audioManager.PlaySFX(trapSound);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.gameObject.CompareTag("Player")) return;
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            animator.SetTrigger("isPressed");
        }
    }
}
