using System;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private AudioClip jumpSound;
    
    [SerializeField] private AudioClip attackSound;
    
    [SerializeField] private AudioClip hurtSound;
    
    [SerializeField] private AudioClip deathSound;
    
    [SerializeField] private AudioClip gameOverSound;
    
    [Header("Footstep Sounds")]
    [SerializeField] private LayerMask footstepSoundLayer;
    [SerializeField] private AudioClip footstepEarthSound;
    [SerializeField] private AudioClip footstepRockSound;
    
    public void PlayAttackSound()
    {
        if (attackSound == null)
        {
            Debug.LogWarning("Attack sound not assigned in the inspector.");
            return;
        }
        audioSource.PlayOneShot(attackSound, 0.5f);
    }
    
    public void PlayHurtSound()
    {
        if (hurtSound == null)
        {
            Debug.LogWarning("Hurt sound not assigned in the inspector.");
            return;
        }
        audioSource.PlayOneShot(hurtSound, 0.5f);
    }
    
    public void PlayDeathSound()
    {
        if (deathSound == null)
        {
            Debug.LogWarning("Death sound not assigned in the inspector.");
            return;
        }
        audioSource.PlayOneShot(deathSound, 0.5f);
        
        if (gameOverSound == null)
        {
            Debug.LogWarning("Game over sound not assigned in the inspector.");
            return;
        }
        audioSource.PlayOneShot(gameOverSound, 0.5f);
    }

    private void ActivateJumpSound()
    {
        if (jumpSound == null)
        {
            Debug.LogWarning("Jump sound not assigned in the inspector.");
            return;
        }
        audioSource.PlayOneShot(jumpSound, 0.5f);
    }
    
    private void PlayFootstepSound(string surfaceType)
    {
        AudioClip selectedClip = null;

        switch (surfaceType)
        {
            case "Earth":
                selectedClip = footstepEarthSound;
                break;
            case "Rock":
                selectedClip = footstepRockSound;
                break;
            default:
                Debug.LogWarning("Unknown surface type: " + surfaceType);
                return;
        }

        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(selectedClip, 0.2f);
    }

    private void Footstep()
    {
        var offset = transform.position + new Vector3(0, 0.5f, 0);
        if (Physics.Raycast(offset, Vector3.down, out var hit, 1f, footstepSoundLayer))
        {
            var surfaceType = hit.collider.tag;
            if (surfaceType is "Earth" or "Rock")
            {
                PlayFootstepSound(surfaceType);
            }
        }
    }
}
