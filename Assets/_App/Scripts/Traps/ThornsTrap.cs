using System;
using UnityEngine;

public class ThornsTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerHealth.TakeDamage(1);
        }
    }
}
