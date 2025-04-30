using System;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    [SerializeField] private Collider cTorch;

    private void Awake()
    {
        DeactivateCollider();
    }

    private void ActivateCollider()
    {
        cTorch.enabled = true;
    }
    
    private void DeactivateCollider()
    {
        cTorch.enabled = false;
    }
}
