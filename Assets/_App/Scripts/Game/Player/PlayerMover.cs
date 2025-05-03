using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover
{
    private static readonly int Magnitude = Animator.StringToHash("InputMagnitude");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private readonly CharacterController _characterController;
    private readonly Animator _animator;
    private readonly Transform _cameraTransform;
    private readonly InputActionReference _move;
    private readonly InputActionReference _sprint;
    private const float RotationSpeed = 720f;

    public Vector3 MovementDirection { get; private set; }
    public float InputMagnitude { get; private set; }

    public PlayerMover(CharacterController cc, Animator anim, Transform cam, 
        InputActionReference move, InputActionReference sprint)
    {
        _characterController = cc;
        _animator = anim;
        _cameraTransform = cam;
        _move = move;
        _sprint = sprint;
    }

    public void HandleMovement()
    {
        var playerMovement = GameSingleton.Instance.PlayerManager.PlayerMovementController;
        if (playerMovement.Attacker.IsAttackingNow || playerMovement.Interactor.IsInteractingNow)
        {
            return;
        }
        
        var input = _move.action.ReadValue<Vector2>();
        MovementDirection = new Vector3(input.x, 0, input.y);

        InputMagnitude = Mathf.Clamp01(MovementDirection.magnitude);
        if (_sprint.action.inProgress)
            InputMagnitude *= 0.5f;

        _animator.SetFloat(Magnitude, InputMagnitude, 0.05f, Time.deltaTime);

        if (MovementDirection != Vector3.zero)
        {
            MovementDirection = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * MovementDirection;
            MovementDirection.Normalize();

            var toRotation = Quaternion.LookRotation(MovementDirection, Vector3.up);
            _characterController.transform.rotation = 
                Quaternion.RotateTowards(
                    _characterController.transform.rotation, toRotation, RotationSpeed * Time.deltaTime);

            _animator.SetBool(IsMoving, true);
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
    }
}