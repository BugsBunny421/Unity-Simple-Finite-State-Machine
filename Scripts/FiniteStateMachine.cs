using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PatrolState))]
[RequireComponent(typeof(IdleState))]
public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField]
    AbstractFSMState startingState;

    AbstractFSMState currentState;
    private void Awake()
    {
        Debug.Log("Awake");
        currentState = null;
    }
    private void Start()
    {
        if (startingState != null)
        {
            ChangeState(startingState);
        }
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void ChangeState(AbstractFSMState nextState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = nextState;
        nextState.EnterState();
    }
}
