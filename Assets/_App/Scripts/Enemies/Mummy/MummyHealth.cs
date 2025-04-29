using System;
using UnityEngine;

public class MummyHealth : MonoBehaviour
{
    // Odin Inspector
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private UIEnemyHealth uiEnemyHealth;

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
        }
        SetHealthUI();
    }

    private void Die()
    {
        var mummy = GetComponent<Mummy>();
        mummy.SetState(new MummyStateDead());
        
        uiEnemyHealth.SetActive(false);
    }

    private void SetHealthUI()
    {
        if (uiEnemyHealth == null) return;
        uiEnemyHealth.SetHealth((float) currentHealth / maxHealth);
    }
}
