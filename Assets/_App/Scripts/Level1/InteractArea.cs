using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InteractArea : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteractEvent;
    [SerializeField] private Transform transformTarget;
    [SerializeField] private bool isUsable = false;
    [ShowInInspector, ReadOnly] private bool _isActive = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isActive == false && isUsable == false) return;
        
        var playerMovementController = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        playerMovementController.CanInteract = true;
        playerMovementController.Interactor.OnStartAction += OnStartInteract;
        playerMovementController.Interactor.OnEndAction += OnEndInteract;
        
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
        uiInteraction.Show();
    }

    private void OnStartInteract()
    {
        if (_isActive == false && isUsable == false) return;
        _isActive = false;

        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
        uiInteraction.Hide();
        
        StartCoroutine(SetPositionPlayer(
            GameSingleton.Instance.PlayerManager.PlayerMovementController.transform,
            transformTarget));
    }
    
    private void OnEndInteract()
    {
        onInteractEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        var playerMovementController = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        playerMovementController.CanInteract = false;
        playerMovementController.Interactor.OnStartAction -= OnStartInteract;
        playerMovementController.Interactor.OnEndAction -= OnEndInteract;
        
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
        uiInteraction.Hide();
    }
    
    private IEnumerator SetPositionPlayer(Transform player, Transform target)
    {
        // Disable the animator to prevent root motion
        var animator = player.GetComponent<Animator>();
        animator.enabled = false;
        
        var startPosition = player.position;
        var startRotation = player.rotation;
    
        var duration = 1f; // total time to complete the move
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var t = Mathf.Clamp01(elapsed / duration);

            player.position = Vector3.Lerp(startPosition, target.position, t);
            player.rotation = Quaternion.Slerp(startRotation, target.rotation, t);

            yield return null;
        }
        
        // Re-enable the animator after the move
        animator.enabled = true;

        // Snap exactly at the end
        player.position = target.position;
        player.rotation = target.rotation;
    }

    private void NextLevel()
    {
        GameSingleton.Instance.GameSceneManager.ChangeLevel("Level2");
    }
    
    private void OnDestroy()
    {
        var playerMovementController = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        if (playerMovementController == null) return;
        playerMovementController.Interactor.OnStartAction -= OnStartInteract;
        playerMovementController.Interactor.OnEndAction -= OnEndInteract;
    }
}
