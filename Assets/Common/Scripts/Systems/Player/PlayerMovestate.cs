



using UnityEngine;

public class PlayerMoveState : StatemachineState<PlayerStatemachine, string>, IStatemachineState
{
    Animator _animator;
    public PlayerMoveState(PlayerStatemachine statemachine, Animator animator) : base(statemachine)
    {
        _animator = animator;
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
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Running_A", 0f);
    }

    public void TransitionExit()
    {
    }
}