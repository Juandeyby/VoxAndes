using System;
using UnityEngine;

public class PlatformTrap : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive = true;
    
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
