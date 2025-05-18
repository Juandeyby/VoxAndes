using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MummyHealth : MonoBehaviour
{
    // Odin Inspector
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private UIEnemyHealth uiEnemyHealth;

    [SerializeField] private AudioClip mummyDeathSound;
    [SerializeField] private AudioClip mummyHurtSound;
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameSingleton.Instance.AudioManager;
    }

    private void Start()
    {
        ConfigHealth();
    }

    private void ConfigHealth()
    {
        currentHealth = maxHealth;
        SetHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTorch"))
        {
            // Assuming the Mummy has a method to take damage
            TakeDamage(25);
        } 
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        } else
        {
            if (mummyHurtSound != null)
            {
                _audioManager.PlaySFX(mummyHurtSound);
            }
            else
            {
                Debug.LogWarning("Mummy attack sound not assigned.");
            }
        }
        SetHealthUI();
    }

    private void Die()
    {
        var mummy = GetComponent<Mummy>();
        mummy.SetState(new MummyStateDead());
        
        if (mummyDeathSound != null)
        {
            _audioManager.PlaySFX(mummyDeathSound);
        }
        else
        {
            Debug.LogWarning("Mummy die sound not assigned.");
        }
        
        uiEnemyHealth.SetActive(false);
    }

    private void SetHealthUI()
    {
        if (uiEnemyHealth == null) return;
        uiEnemyHealth.SetHealth((float) currentHealth / maxHealth);
    }
}
