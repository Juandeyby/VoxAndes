using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public bool CanPlay { get; set; } = true;
    public bool CanInteract { get; set; } = false;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    public PlayerJumper Jumper => _jumper;
    private PlayerAttacker _attacker;
    public PlayerAttacker Attacker => _attacker;
    private PlayerInteractor _interactor;
    public PlayerInteractor Interactor => _interactor;
    
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        var characterController = GetComponent<CharacterController>();

        _mover = new PlayerMover(characterController, _animator, cameraTransform, move, sprint);
        _jumper = new PlayerJumper(characterController, _animator, jump);
        _attacker = new PlayerAttacker(_animator);
        _interactor = new PlayerInteractor(_animator);
    }

    public void ResetMovement()
    {
        
    }

    public void Die()
    {
        CanPlay = false;
        _animator.CrossFade("Die", 0.2f);
        
        var audioManager = GameSingleton.Instance.AudioManager;
        audioManager.EndLevel();

        var player = GameSingleton.Instance.PlayerManager.Player;
        player.ResetPlayer();
    }

    void Update()
    {
        if (!CanPlay) return;

        // Update states
        _jumper.UpdateState();     // e.g. check if landed, update IsJumping
        _attacker.UpdateState();   // e.g. check attack cooldowns
        _interactor.UpdateState(); // if needed

        // Handle jump input first (allowed any time unless already jumping)
        _jumper.HandleJump();

        // Then handle attack (only if not jumping)
        _attacker.HandleAttack(attack);

        // Then interaction (if allowed)
        if (CanInteract)
        {
            _interactor.HandleInteract(interact);
        }

        // Movement allowed as long as not locked (maybe jumping or attacking movement is okay)
        _mover.HandleMovement();
    }

    void OnAnimatorMove()
    {
        _jumper.ApplyRootMotion();
    }

    void OnApplicationFocus(bool focus)
    {
        var gameSceneManager = GameSingleton.Instance.GameSceneManager;
        if (gameSceneManager.IsGamePaused)
        {
            return;
        }
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void Init()
    {
        StartCoroutine(ResetPlayer());
    }
    
    private IEnumerator ResetPlayer()
    {
        _animator.enabled = false;
        yield return null;
        
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        Debug.Log("Player reset position and rotation" + transform.position + " " + transform.rotation);

        yield return null;
        
        _animator.enabled = true;
        _animator.CrossFade("Breathing Idle", 0.2f);
        
        CanPlay = true;
        CanInteract = false;
    }
}