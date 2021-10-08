using System;
using UnityEngine;

namespace Assets.FiniteStateMachine.States
{
    class PatrolState : State
    {
        [SerializeField] float patrolRadius;
        [SerializeField] float walkSpeed = 1.2f;
        [SerializeField] float remainingDistanceCheck = 0.5f;
        [SerializeField] IdleState idleState;
        [SerializeField] ChaseState chaseState;
        public override void EnterState(FiniteStateMachine StateMachine)
        {
            base.EnterState(StateMachine);
            StateMachine.Agent.SetDestination(Utilities.NavmeshUtilities.RandomNavmeshLocation(patrolRadius, StateMachine.transform));
            StateMachine.SetAnimationState(AnimationStates.WALKING);
            StateMachine.Agent.speed = walkSpeed;
        }

        public override void ExitState(FiniteStateMachine StateMachine)
        {
            base.ExitState(StateMachine);
        }

        public override State UpdateState(FiniteStateMachine StateMachine)
        {
            if (StateMachine.LineOfSightComponent.Target != null)
            {
                return chaseState;
            }
            else if (StateMachine.Agent.remainingDistance <= remainingDistanceCheck)
            {
                return idleState;
            }
            else
            {
                return this;
            }
        }
    }
}
