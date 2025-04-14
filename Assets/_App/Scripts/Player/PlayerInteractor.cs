using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor
{
    private readonly Animator _animator;
    private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

    private bool _isInteracting;
    private const float InteractionDuration = 4.0f; 
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
            _isInteracting = true;
            _lastInteractionTime = Time.time;
            _animator.SetBool(IsInteracting, true);

            // Aquí puedes lanzar eventos, raycasts, etc.
        }

        if (_isInteracting && Time.time - _lastInteractionTime >= InteractionDuration)
        {
            _isInteracting = false;
            _animator.SetBool(IsInteracting, false);
        }
    }

    private bool CanInteract()
    {
        return !_isInteracting;
    }
}
