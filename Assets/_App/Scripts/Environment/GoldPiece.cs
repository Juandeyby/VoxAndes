using System;
using UnityEngine;

public class GoldPiece : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    private void Start()
    {
        rb.AddExplosionForce(
            1000f, 
            transform.position + Vector3.up * 2f, 
            5f, 
            1f, 
            ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerGold = other.GetComponent<PlayerGold>();
            if (playerGold != null)
            {
                playerGold.AddGold(1);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("PlayerGold component not found on the Player GameObject.");
            }
        }
    }

    private void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
