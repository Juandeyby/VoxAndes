using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var playerMovementController = other.GetComponent<PlayerMovementController>();
        if (playerMovementController == null) return;
        playerMovementController.Interactor.OnInteractAction += value =>
        {
            NextLevel();
            var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
            uiInteraction.Hide();
        };
        playerMovementController.CanInteract = true;
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
        uiInteraction.Show();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var playerMovementController = other.GetComponent<PlayerMovementController>();
        if (playerMovementController == null) return;
        playerMovementController.Interactor.OnInteractAction -= value => 
        {
            NextLevel();
            var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
            uiInteraction.Hide();
        };
        playerMovementController.CanInteract = false;
        var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
        uiInteraction.Hide();
    }

    private void NextLevel()
    {
        GameSingleton.Instance.GameSceneManager.ChangeLevel("Level2");
    }
    
    private void OnDestroy()
    {
        var playerMovementController = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        if (playerMovementController == null) return;
        playerMovementController.Interactor.OnInteractAction -= value => 
        {
            var uiInteraction = GameSingleton.Instance.UIGameManager.HubMenu.Instruction;
            uiInteraction.Hide();
        };
    }
}
