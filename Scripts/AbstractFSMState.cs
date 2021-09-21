using UnityEngine;

public abstract class AbstractFSMState : MonoBehaviour
{
    public virtual void OnEnable() { }
    public virtual void EnterState()
    {
    }
    public abstract void UpdateState();
    public virtual void ExitState()
    {
    }
}
