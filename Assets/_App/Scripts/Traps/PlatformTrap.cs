using System;
using UnityEngine;

public class PlatformTrap : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            animator.SetTrigger("isPressed");
        }
    }
}
