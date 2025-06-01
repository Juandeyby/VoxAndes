using System;
using UnityEngine;

public class WallTorch : MonoBehaviour
{
    [SerializeField] private Light torchLight;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private AudioClip fireSound;

    private void Awake()
    {
        if (torchLight != null)
        {
            torchLight.enabled = false;
        }
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }
    }

    public void TurnOnTorch()
    {
        if (torchLight != null)
        {
            torchLight.enabled = true;
        }
        if (fireParticles != null)
        {
            fireParticles.Play();
        }
        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }
    
    public void TurnOffTorch()
    {
        if (torchLight != null)
        {
            torchLight.enabled = false;
        }
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }
    }
}
