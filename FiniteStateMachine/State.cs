using UnityEngine;
using UnityEngine.AI;

namespace Assets.FiniteStateMachine
{
    public abstract class State : MonoBehaviour
    {
        public virtual void OnEnable() { }
        public virtual void EnterState(FiniteStateMachine StateMachine) { }
        public abstract State UpdateState(FiniteStateMachine StateMachine);
        public virtual void ExitState(FiniteStateMachine StateMachine) { }
    }
}
