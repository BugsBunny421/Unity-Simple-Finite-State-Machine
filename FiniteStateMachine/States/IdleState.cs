using UnityEngine;

namespace Assets.FiniteStateMachine.States
{
    class IdleState : State
    {
        [SerializeField] float idleTime;
        [SerializeField] PatrolState patrolState;
        [SerializeField] ChaseState chaseState;

        private float currentIdleTime;

        public override void EnterState(FiniteStateMachine StateMachine)
        {
            base.EnterState(StateMachine);
            StateMachine.Agent.SetDestination(StateMachine.transform.position);
            StateMachine.SetAnimationState(AnimationStates.IDLE);
            currentIdleTime = 0f;
        }

        public override void ExitState(FiniteStateMachine StateMachine)
        {
            base.ExitState(StateMachine);
        }

        public override State UpdateState(FiniteStateMachine StateMachine)
        {
            currentIdleTime += Time.deltaTime;
            if (StateMachine.LineOfSightComponent.Target != null)
            {
                return chaseState;
            }
            else if (currentIdleTime >= idleTime)
            {
                return patrolState;
            }
            else
            {
                return this;
            }
        }
    }
}
