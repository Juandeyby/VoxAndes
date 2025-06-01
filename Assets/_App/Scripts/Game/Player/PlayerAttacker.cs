using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private readonly Animator _animator;
    private bool _isAttacking;
    private const float AttackCooldown = 1.8f;
    private float _lastAttackTime = -999f;
    public bool IsAttackingNow => _isAttacking;

    public PlayerAttacker(Animator animator)
    {
        _animator = animator;
    }

    public void HandleAttack(InputAction attackAction)
    {
        var playerMovement = GameSingleton.Instance.PlayerManager.Player.PlayerMovementController;
        if (playerMovement.Jumper.IsJumpingNow || playerMovement.Interactor.IsInteractingNow)
        {
            return;
        }
        
        // Check for input and if not currently attacking
        if (attackAction.WasPressedThisFrame() && !_isAttacking)
        {
            _animator.SetTrigger(IsAttacking);
            _isAttacking = true;
            _lastAttackTime = Time.time;
        }
    }

    public void UpdateState()
    {
        if (_isAttacking && Time.time - _lastAttackTime >= AttackCooldown)
        {
            _isAttacking = false;
        } 
    }
}
