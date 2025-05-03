using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor
{
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
    private readonly Animator _animator;
    private const float InteractionCooldown = 4.45f; 
    private float _lastInteractionTime = -999f;

    public Action OnStartAction { get; set; }
    public Action OnEndAction { get; set; }
    public bool IsInteractingNow { get; set; }

    public PlayerInteractor(Animator animator)
    {
        _animator = animator;
    }

    public void HandleInteract(InputAction interactAction)
    {
        var playerMovement = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        if (playerMovement.Jumper.IsJumpingNow || playerMovement.Attacker.IsAttackingNow)
        {
            return;
        }

        if (interactAction.WasPressedThisFrame() && !IsInteractingNow)
        {
            _animator.SetTrigger(IsInteracting);
            _lastInteractionTime = Time.time;
            
            IsInteractingNow = true;
            
            OnStartAction?.Invoke();
            OnStartAction = null;
        }
    }

    public void UpdateState()
    {
        if (Time.time - _lastInteractionTime >= InteractionCooldown)
        {
            OnEndAction?.Invoke();
            OnEndAction = null;
            
            IsInteractingNow = false;
        }
    }
}
