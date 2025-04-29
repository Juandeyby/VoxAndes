using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Mummy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController => characterController;
    [SerializeField] private UnityEvent onDeath;
    public UnityEvent OnDeath => onDeath;
    [SerializeField] private Collider attackCollider;
    public Collider AttackCollider => attackCollider;
    private IMummyState _currentState;

    private void Start()
    {
        // Initialize the Mummy's state
        SetState(new MummyStateIdle());
    }

    public void SetState(IMummyState newState)
    {
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }
    
    private void Update()
    {
        _currentState?.Update(this);
    }
}

public class MummyStateIdle : IMummyState
{
    public void Enter(Mummy mummy)
    {
        mummy.Animator.SetTrigger("Idle");
    }

    public void Update(Mummy mummy)
    {
        // Logic for updating idle state
        var playerPosition = GameSingleton.Instance.PlayerManager.PlayerMovementController.transform.position;
        var navMeshAgent = mummy.Agent;
        
        if (Vector3.Distance(mummy.transform.position, playerPosition) < 5f)
        {
            mummy.SetState(new MummyStateChase());
        }
    }

    public void Exit(Mummy mummy)
    {
        // Logic for exiting idle state
    }
}

public class MummyStateChase : IMummyState
{
    public void Enter(Mummy mummy)
    {
        mummy.Animator.SetTrigger("Walk");
    }

    public void Update(Mummy mummy)
    {
        // Lógica para actualizar el estado de persecución
        var playerPosition = GameSingleton.Instance.PlayerManager.PlayerMovementController.transform.position;
        var navMeshAgent = mummy.Agent;
        
        // Verificar la distancia entre la momia y el jugador
        if (Vector3.Distance(mummy.transform.position, playerPosition) <= 1.2f)
        {
            mummy.SetState(new MummyStateAttack());
            return;
        }

        // Obtener la dirección hacia el jugador
        var direction = (playerPosition - mummy.transform.position).normalized;

        // Rote la momia solo hacia la dirección del jugador
        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); 
            mummy.transform.rotation = Quaternion.Slerp(mummy.transform.rotation, targetRotation, Time.deltaTime * 5f); // Ajusta la velocidad de rotación aquí
        }

        // Mover la momia hacia el jugador
        navMeshAgent.SetDestination(playerPosition);
    }

    public void Exit(Mummy mummy)
    {
        // Lógica para salir del estado de persecución
    }
}


public class MummyStateAttack : IMummyState
{
    public void Enter(Mummy mummy)
    {
        mummy.Animator.SetTrigger("Attack");
        mummy.AttackCollider.enabled = true;
    }

    public void Update(Mummy mummy)
    {
        // Logic for updating attack state
        var playerPosition = GameSingleton.Instance.PlayerManager.PlayerMovementController.transform.position;
        var navMeshAgent = mummy.Agent;

        if (Vector3.Distance(mummy.transform.position, playerPosition) > 2f)
        {
            mummy.SetState(new MummyStateChase());
            return;
        }
        
        // Attack logic here
    }

    public void Exit(Mummy mummy)
    {
        // Logic for exiting attack state
        mummy.AttackCollider.enabled = false;
    }
}

public class MummyStateDead : IMummyState
{
    public void Enter(Mummy mummy)
    {
        mummy.Animator.SetTrigger("Die");
        mummy.Agent.isStopped = true;
        
        mummy.CharacterController.enabled = false;
        
        mummy.OnDeath?.Invoke();
    }

    public void Update(Mummy mummy)
    {
        // Logic for updating dead state
    }

    public void Exit(Mummy mummy)
    {
        // Logic for exiting dead state
    }
}

public interface IMummyState
{
    void Enter(Mummy mummy);
    void Update(Mummy mummy);
    void Exit(Mummy mummy);
}
