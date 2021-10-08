using System;
using UnityEngine;

namespace Assets.FiniteStateMachine.States
{
    class ChaseState : State
    {
        [SerializeField] IdleState idleState;
        [SerializeField] AttackState attackState;
        [SerializeField] float chaseSpeed;
        public override void EnterState(FiniteStateMachine StateMachine)
        {
            base.EnterState(StateMachine);
            StateMachine.Agent.speed = chaseSpeed;
            StateMachine.SetAnimationState(AnimationStates.RUNNING);
        }

        public override State UpdateState(FiniteStateMachine StateMachine)
        {
            if (StateMachine.LineOfSightComponent.Target != null)
            {
                if (Vector3.Distance(StateMachine.Agent.transform.position, StateMachine.LineOfSightComponent.Target.transform.position) <= attackState.attackRadius)
                {
                    return attackState;
                }
                else
                {
                    StateMachine.Agent.SetDestination(StateMachine.LineOfSightComponent.Target.position);
                    return this;
                }
            }
            else
            {
                return idleState;
            }
        }
    }
}
