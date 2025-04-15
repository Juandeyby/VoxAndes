using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor
{
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
    private readonly Animator _animator;
    private bool _isInteracting;
    private const float InteractionCooldown = 4.0f; 
    private float _lastInteractionTime = -999f;

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

            // Aquí puedes lanzar eventos, raycasts, etc.
        }

        if (_isInteracting && Time.time - _lastInteractionTime >= InteractionCooldown)
        {
            _animator.SetBool(IsInteracting, false);
            _isInteracting = false;
        }
    }

    private bool CanInteract()
    {
        return !_isInteracting;
    }
}
