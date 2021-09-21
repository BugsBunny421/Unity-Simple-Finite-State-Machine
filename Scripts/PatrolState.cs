using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PatrolState : AbstractFSMState
{
    [SerializeField] float patrolRadius = 20f;
    [SerializeField] float remainingDistanceCheck = 1f;
    public override void OnEnable()
    {
        base.OnEnable();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        FiniteStateMachine = GetComponent<FiniteStateMachine>();
    }
    public override void EnterState()
    {
        NavMeshAgent.isStopped = false;
        NavMeshAgent.SetDestination(NavmeshUtilities.RandomNavmeshLocation(patrolRadius, transform));
        Debug.Log("Entered Patrol State");
        base.EnterState();
    }
    public override void UpdateState()
    {
        if (NavMeshAgent.remainingDistance <= remainingDistanceCheck)
        {
            FiniteStateMachine.ChangeState(GetComponent<IdleState>());
        }
    }
    public override void ExitState()
    {
        Debug.Log("Exiting Patrol State");
        base.ExitState();
    }

    public NavMeshAgent NavMeshAgent { get; private set; }
    public FiniteStateMachine FiniteStateMachine { get; private set; }
}
