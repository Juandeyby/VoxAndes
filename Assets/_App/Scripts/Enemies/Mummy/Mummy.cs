using System;
using UnityEngine;
using UnityEngine.AI;

public class Mummy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
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
        // Logic for updating chase state
        var playerPosition = GameSingleton.Instance.PlayerManager.PlayerMovementController.transform.position;
        var navMeshAgent = mummy.Agent;
        
        if (Vector3.Distance(mummy.transform.position, playerPosition) <= 1.5f)
        {
            mummy.SetState(new MummyStateAttack());
            return;
        }
        navMeshAgent.SetDestination(playerPosition);
    }

    public void Exit(Mummy mummy)
    {
        // Logic for exiting chase state
    }
}

public class MummyStateAttack : IMummyState
{
    public void Enter(Mummy mummy)
    {
        mummy.Animator.SetTrigger("Attack");
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
    }
}

public interface IMummyState
{
    void Enter(Mummy mummy);
    void Update(Mummy mummy);
    void Exit(Mummy mummy);
}
