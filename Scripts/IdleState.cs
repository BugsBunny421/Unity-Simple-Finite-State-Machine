using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class IdleState : AbstractFSMState
{
    [SerializeField] float idleTime = 5f;
    float currentIdleTime;
    public override void OnEnable()
    {
        base.OnEnable();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        FiniteStateMachine = GetComponent<FiniteStateMachine>();
    }
    public override void EnterState()
    {
        currentIdleTime = 0f;
        NavMeshAgent.isStopped = true;
        Debug.Log("Entered Idle State");
        base.EnterState();
    }

    public override void UpdateState()
    {
        currentIdleTime += Time.deltaTime;
        if (currentIdleTime >= idleTime)
        {
            FiniteStateMachine.ChangeState(GetComponent<PatrolState>());
        }
    }
    public override void ExitState()
    {
        Debug.Log("EXITING IDLE STATE");
        NavMeshAgent.isStopped = false;
        base.ExitState();
    }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public FiniteStateMachine FiniteStateMachine { get; private set; }
}
