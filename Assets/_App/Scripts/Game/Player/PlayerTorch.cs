using System;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    [SerializeField] private Collider cTorch;
    [SerializeField] private AudioClip manAttackSound;

    private void Awake()
    {
        DeactivateCollider();
    }

    private void ActivateCollider()
    {
        cTorch.enabled = true;
    }
    
    private void ActivateSound()
    {
        if (manAttackSound != null)
        {
            AudioSource.PlayClipAtPoint(manAttackSound, transform.position);
        }
    }
    
    private void DeactivateCollider()
    {
        cTorch.enabled = false;
    }
}
