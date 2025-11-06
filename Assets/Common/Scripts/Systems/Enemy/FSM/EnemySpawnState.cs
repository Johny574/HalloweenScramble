



using UnityEngine;

public class EnemySpawnState : StatemachineState<EnemyStatemachine, string>,IStatemachineState 
{
    Animator _animator;
    public EnemySpawnState(EnemyStatemachine statemachine, Animator animator) : base(statemachine)
    {
        _animator = animator;
    }

    public bool GetTransitionCondition()
    {
        throw new System.NotImplementedException();
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
        
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Spawn_Ground", 0f);
    }

    public void TransitionExit()
    {
    }
}