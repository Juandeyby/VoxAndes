using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform cameraTransform;

    [Header("Input")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference sprint;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference interact;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    private PlayerAttacker _attacker;
    private PlayerInteractor _interactor;

    void Awake()
    {
        var animator = GetComponent<Animator>();
        var characterController = GetComponent<CharacterController>();

        _mover = new PlayerMover(characterController, animator, cameraTransform, move, sprint);
        _jumper = new PlayerJumper(characterController, animator, jump);
        _attacker = new PlayerAttacker(animator);
        _interactor = new PlayerInteractor(animator);
    }

    void Update()
    {
        if (!_attacker.IsAttackingNow && !_interactor.IsInteractingNow)
        {
            _mover.HandleMovement();
            _jumper.HandleJump();
        }
        if (!_jumper.IsJumpingNow)
        {
            _attacker.HandleAttack(attack);
            _interactor.HandleInteract(interact);
        }
    }

    void OnAnimatorMove()
    {
        _jumper.ApplyRootMotion();
    }

    void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
