using System;
using UnityEngine;

public class MummyHand : MonoBehaviour
{
    [SerializeField] private Collider cHand;

    private void Awake()
    {
        DeactivateCollider();   
    }

    private void ActivateCollider()
    {
        cHand.enabled = true;
    }
    
    private void DeactivateCollider()
    {
        cHand.enabled = false;
    }
}
