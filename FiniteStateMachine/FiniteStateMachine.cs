using Assets.FiniteStateMachine.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.FiniteStateMachine
{
    public class FiniteStateMachine : MonoBehaviour
    {
        public State startingState;
        State currentState;
        AnimationStates currentAnimationState;
        public NavMeshAgent Agent { get; set; }
        public LineOfSight LineOfSightComponent { get; set; }
        private Animator AnimatorComponent { get; set; }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            LineOfSightComponent = GetComponentInChildren<LineOfSight>();
            AnimatorComponent = GetComponentInChildren<Animator>();
            currentState = null;
        }
        private void Start()
        {
            if (Agent == null || LineOfSightComponent == null || AnimatorComponent == null)
            {
                if (Agent == null) { Debug.LogError("No Agent Found", this); }
                if (LineOfSightComponent == null) { Debug.LogError("No Line of Sight Found", this); }
                if (AnimatorComponent == null) { Debug.LogError("No Animator Found", this); }
                return; //Don't Start State Machine without these Components
            }
            if (startingState != null)
            {
                EnterState(startingState);
            }
        }
        private void Update()
        {
            State nextState = currentState?.UpdateState(this);
            if (nextState != null && nextState != currentState)
            {
                EnterState(nextState);
            }
        }

        private void EnterState(State nextState)
        {
            if (currentState != null)
            {
                currentState.ExitState(this);
            }
            currentState = nextState;
            nextState.EnterState(this);
        }
        public void SetAnimationState(AnimationStates nextState)
        {
            if (nextState == currentAnimationState)
            {
                Debug.LogWarning("You are trying to set an Animation State which is Already playing, This may impact Performance");
                //return;
            }
            AnimatorComponent.SetBool("Idle", false);
            AnimatorComponent.SetBool("Walking", false);
            AnimatorComponent.SetBool("Running", false);
            switch (nextState)
            {
                case AnimationStates.IDLE:
                    AnimatorComponent.SetBool("Idle", true);
                    break;
                case AnimationStates.WALKING:
                    AnimatorComponent.SetBool("Walking", true);
                    break;
                case AnimationStates.RUNNING:
                    AnimatorComponent.SetBool("Running", true);
                    break;
                default:
                    Debug.LogError($"You are trying to set Incorrect Animation State. State Name: \"{nextState}\"", this);
                    break;
            }
            currentAnimationState = nextState;
        }
        public void SetAnimationTrigger(AnimationTriggers trigger)
        {
            switch (trigger)
            {
                case AnimationTriggers.ATTACK:
                    AnimatorComponent.SetTrigger("Attack");
                    break;
                default:
                    break;
            }
        }
    }
    public enum AnimationStates
    {
        IDLE,
        WALKING,
        RUNNING,
    }
    public enum AnimationTriggers
    {
        ATTACK,
    }
}