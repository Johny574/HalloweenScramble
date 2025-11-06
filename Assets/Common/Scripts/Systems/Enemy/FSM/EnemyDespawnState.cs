
using UnityEngine;

public class EnmeyDespawnState: StatemachineState<EnemyStatemachine, string>,IStatemachineState 
{
    Animator _animator;
    public EnmeyDespawnState(EnemyStatemachine statemachine, Animator animator) : base(statemachine)
    {
    }

    public bool GetTransitionCondition()
    {
        return false; 
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
            _statemachine.gameObject.SetActive(false);

        
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Death_A", 0f);
    }

    public void TransitionExit()
    {
    }
}