using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumper
{
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private readonly CharacterController _characterController;
    private readonly Animator _animator;
    private readonly InputActionReference _jump;

    private float _ySpeed;
    private readonly float _originalStepOffset;
    private float? _lastGroundedTime;
    private float? _jumpPressedTime;

    private const float JumpSpeed = 7f;
    private const float JumpHorizontalSpeed = 3f;
    private const float JumpButtonGracePeriod = 0.2f;
    private bool _isGrounded;
    private bool _isJumping;
    public bool IsJumpingNow => _isJumping;

    public PlayerJumper(CharacterController cc, Animator anim, InputActionReference jump)
    {
        _characterController = cc;
        _animator = anim;
        this._jump = jump;
        _originalStepOffset = cc.stepOffset;
    }

    public void HandleJump()
    {
        var playerMovement = GameSingleton.Instance.PlayerManager.Player.PlayerMovementController;
        if (playerMovement.Attacker.IsAttackingNow || playerMovement.Interactor.IsInteractingNow)
        {
            return;
        }
        
        _ySpeed += Physics.gravity.y * Time.deltaTime;

        if (_characterController.isGrounded)
            _lastGroundedTime = Time.time;

        if (_jump.action.triggered)
            _jumpPressedTime = Time.time;

        if (Time.time - _lastGroundedTime <= JumpButtonGracePeriod)
        {
            _characterController.stepOffset = _originalStepOffset;
            _ySpeed = -0.5f;
            _isGrounded = true;
            _isJumping = false;
            _animator.SetBool(IsGrounded, true);
            _animator.SetBool(IsJumping, false);
            _animator.SetBool(IsFalling, false);

            if (Time.time - _jumpPressedTime <= JumpButtonGracePeriod)
            {
                _ySpeed = JumpSpeed;
                _isJumping = true;
                _jumpPressedTime = null;
                _lastGroundedTime = null;
                _animator.SetBool(IsJumping, true);
            }
        }
        else
        {
            _characterController.stepOffset = 0;
            _isGrounded = false;
            _animator.SetBool(IsGrounded, false);

            if ((_isJumping && _ySpeed < 0) || _ySpeed < -2f)
                _animator.SetBool(IsFalling, true);
        }

        var horizontalVelocity = Vector3.zero;
        if (!_isGrounded)
        {
            horizontalVelocity = new Vector3(
                _characterController.transform.forward.x, 0, _characterController.transform.forward.z) *
                                 JumpHorizontalSpeed;
        }

        var velocity = horizontalVelocity;
        velocity.y = _ySpeed;

        _characterController.Move(velocity * Time.deltaTime);
    }

    
    public void UpdateState()
    {
        if (_characterController.isGrounded)
        {
            // Player is on the ground → reset jump state
            if (_isJumping)
            {
                _isJumping = false;
                _animator.SetBool(IsJumping, false);
                _animator.SetBool(IsFalling, false);
            }

            _isGrounded = true;
            _animator.SetBool(IsGrounded, true);
        }
        else
        {
            _isGrounded = false;
            _animator.SetBool(IsGrounded, false);

            if (_isJumping && _ySpeed < 0)
            {
                // Jumping, but falling down
                _animator.SetBool(IsFalling, true);
            }
        }
    }
    
    public void ApplyRootMotion()
    {
        if (!_isGrounded) return;

        var velocity = _animator.deltaPosition;
        velocity.y = _ySpeed * Time.deltaTime;
        _characterController.Move(velocity);
    }
}