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
        if (attackAction.triggered && CanAttack())
        {
            _animator.SetBool(IsAttacking, true);
            _isAttacking = true;
            _lastAttackTime = Time.time;
        }

        if (_isAttacking && Time.time - _lastAttackTime >= AttackCooldown)
        {
            _animator.SetBool(IsAttacking, false);
            _isAttacking = false;
        }
    }

    private bool CanAttack()
    {
        return !_isAttacking;
    }
}

