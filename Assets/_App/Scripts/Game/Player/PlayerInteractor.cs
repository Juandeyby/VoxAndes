using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor
{
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
    private readonly Animator _animator;
    private bool _isInteracting;
    private const float InteractionCooldown = 4.0f; 
    private float _lastInteractionTime = -999f;

    public Action<bool> OnInteractAction { get; set; }

    public bool IsInteractingNow => _isInteracting;

    public PlayerInteractor(Animator animator)
    {
        _animator = animator;
    }

    public void HandleInteract(InputAction interactAction)
    {
        if (interactAction.triggered && CanInteract())
        {
            _animator.SetBool(IsInteracting, true);
            _isInteracting = true;
            _lastInteractionTime = Time.time;
        }

        if (_isInteracting && Time.time - _lastInteractionTime >= InteractionCooldown)
        {
            OnInteractAction?.Invoke(true);
            
            _animator.SetBool(IsInteracting, false);
            _isInteracting = false;
        }
    }

    private bool CanInteract()
    {
        return !_isInteracting;
    }
}
