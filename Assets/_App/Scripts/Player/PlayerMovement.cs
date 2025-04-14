using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int InputMagnitude = Animator.StringToHash("InputMagnitude");
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;
    [SerializeField] private InputActionReference attack;
    
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float jumpSpeed;
    
    [SerializeField]
    private float jumpHorizontalSpeed = 0.5f;

    [SerializeField]
    private float jumpButtonGracePeriod;

    [SerializeField]
    private Transform cameraTransform;

    private Animator _animator;
    private CharacterController _characterController;
    private float _ySpeed;
    private float _originalStepOffset;
    private float? _lastGroundedTime;
    private float? _jumpButtonPressedTime;
    private bool _isJumping;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _originalStepOffset = _characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        var inputDirection = move.action.ReadValue<Vector2>();

        var movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        var inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        
        if (sprint.action.inProgress)
        {
            inputMagnitude /= 2;
        }

        _animator.SetFloat(InputMagnitude, inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) *
                            movementDirection;
        movementDirection.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime;

        if (_characterController.isGrounded)
        {
            _lastGroundedTime = Time.time;
        }
        
        if (jump.action.triggered)
        {
            Debug.Log("Jumping");
            _jumpButtonPressedTime = Time.time;
        }
        
        if (attack.action.triggered)
        {
            Debug.Log("Attacking");
            _animator.SetBool(IsAttacking, true);
        }

        if (Time.time - _lastGroundedTime <= jumpButtonGracePeriod)
        {
            _characterController.stepOffset = _originalStepOffset;
            _ySpeed = -0.5f;
            _animator.SetBool(IsGrounded, true);
            _isGrounded = true;
            _animator.SetBool(IsJumping, false);
            _isJumping = false;
            _animator.SetBool(IsFalling, false);

            if (Time.time - _jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                _ySpeed = jumpSpeed;
                _animator.SetBool(IsJumping, true);
                _jumpButtonPressedTime = null;
                _lastGroundedTime = null;
            }
        }
        else
        {
            _characterController.stepOffset = 0;
            _animator.SetBool(IsGrounded, false);
            _isGrounded = false;
            
            if ((_isJumping && _ySpeed < 0) || _ySpeed < -2f)
            {
                _animator.SetBool(IsFalling, true);
            }
        }

        if (movementDirection != Vector3.zero)
        {
            _animator.SetBool(IsMoving, true);

            var toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
        
        if (_isGrounded == false)
        {
            var velocity = movementDirection * (inputMagnitude * jumpHorizontalSpeed);
            velocity.y = _ySpeed;
            
            _characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (_isGrounded == false) return;
        
        var velocity = _animator.deltaPosition;
        velocity.y = _ySpeed * Time.deltaTime;

        _characterController.Move(velocity);
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
