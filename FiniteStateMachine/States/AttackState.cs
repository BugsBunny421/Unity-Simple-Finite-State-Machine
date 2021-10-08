using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.FiniteStateMachine.States
{
    class AttackState : State
    {
        [SerializeField] ChaseState chaseState;
        [SerializeField] IdleState idleState;
        [SerializeField] float timeBetweenAttacks = 3f;

        /*It's Public because it'll be used by chaseState*/
        public float attackRadius;
        float timeSinceLastAttack;

        public override void EnterState(FiniteStateMachine StateMachine)
        {
            base.EnterState(StateMachine);
            timeSinceLastAttack = 0;
            StateMachine.Agent.SetDestination(StateMachine.Agent.transform.position);
            StateMachine.SetAnimationState(AnimationStates.IDLE);
        }

        public override void ExitState(FiniteStateMachine StateMachine)
        {
            base.ExitState(StateMachine);
        }

        public override State UpdateState(FiniteStateMachine StateMachine)
        {
            if (StateMachine.LineOfSightComponent.Target != null)
            {
                if (Vector3.Distance(StateMachine.Agent.transform.position, StateMachine.LineOfSightComponent.Target.transform.position) <= attackRadius)
                {
                    if (timeSinceLastAttack >= timeBetweenAttacks)
                    {
                        Attack();
                        StateMachine.SetAnimationTrigger(AnimationTriggers.ATTACK);
                        timeSinceLastAttack = 0;
                    }
                    else
                    {
                        timeSinceLastAttack += Time.deltaTime;
                    }
                    return this;
                }
                else
                {
                    return chaseState;
                }
            }
            else
            {
                return idleState;
            }
        }
        private void Attack()
        {

            Debug.Log("Attacking");
        }
    }
}
