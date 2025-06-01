using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int currentHealth;
    private UILives _uiLives;

    private PlayerSound _playerSound;
    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        if (_playerMovementController == null)
        {
            Debug.LogWarning("PlayerMovementController component not found on the Player GameObject.");
        }
        _playerSound = GetComponent<PlayerSound>();
        if (_playerSound == null)
        {
            Debug.LogWarning("PlayerSound component not found on the Player GameObject.");
        }
    }

    private void Start()
    {
        _uiLives = GameSingleton.Instance.UIGameManager.HubMenu.Lives;
        Init();
    }
    
    public void Init()
    {
        currentHealth = maxHealth;
        SetHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHand"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (_playerSound != null)
            {
                _playerSound.PlayHurtSound();
            }
            else
            {
                Debug.LogWarning("Player hurt sound not assigned.");
            }
        }
        SetHealthUI();
    }

    private void Die()
    {
        _playerMovementController.Die();
    }

    private void SetHealthUI()
    {
        if (_uiLives == null) return;
        _uiLives.SetLives(currentHealth);
    }
}
