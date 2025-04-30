using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int currentHealth;
    private UILives _uiLives;

    private void Start()
    {
        _uiLives = GameSingleton.Instance.UIGameManager.HubMenu.Lives;
        ConfigHealth();
    }
    
    private void ConfigHealth()
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

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        SetHealthUI();
    }

    private void Die()
    {
        
    }

    private void SetHealthUI()
    {
        if (_uiLives == null) return;
        _uiLives.SetLives(currentHealth);
    }
}
