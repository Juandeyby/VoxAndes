using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;
    
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

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        var inputDirection = move.action.ReadValue<Vector2>();

        Vector3 movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        
        if (sprint.action.inProgress)
        {
            inputMagnitude /= 2;
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        
        if (jump.action.triggered)
        {
            Debug.Log("Jumping");
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;
            
            if ((isJumping && ySpeed < 0) || ySpeed < -2f)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        
        if (isGrounded == false)
        {
            var velocity = movementDirection * (inputMagnitude * jumpHorizontalSpeed);
            velocity.y = ySpeed;
            
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (isGrounded == false) return;
        
        Vector3 velocity = animator.deltaPosition;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
