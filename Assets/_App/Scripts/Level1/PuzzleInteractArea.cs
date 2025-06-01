using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PuzzleInteractArea : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteractEvent;
    [SerializeField] private bool isUsable = false;
    [ShowInInspector, ReadOnly] private bool _isActive = true;
    
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isActive == false && isUsable == false) return;
        
        var playerMovementController = GameSingleton.Instance.PlayerManager.Player.PlayerMovementController;
        playerMovementController.CanInteract = true;
        
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.UIPuzzleInteract;
        uiInteraction.Config(sprite1, sprite2, sprite3);
        uiInteraction.Show();
    }

    private void OnTriggerExit(Collider other)
    {
        var playerMovementController = GameSingleton.Instance.PlayerManager.Player.PlayerMovementController;
        playerMovementController.CanInteract = false;
        
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.UIPuzzleInteract;
        uiInteraction.Hide();
    }
}