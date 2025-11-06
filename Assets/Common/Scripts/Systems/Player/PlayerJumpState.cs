

using UnityEngine;

public class PlayerJumpState : StatemachineState<PlayerStatemachine, string>, IStatemachineState
{
    Animator _animator;
    public PlayerJumpState(PlayerStatemachine statemachine, Animator animator) : base(statemachine)
    {
        _animator = animator;
    }

    public bool GetTransitionCondition()
    {
        var currentState = _animator.GetCurrentAnimatorStateInfo(0);
        if (!currentState.IsName("Jump_Start") || currentState.normalizedTime < .9f)
            return false;

        return true;
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Jump_Idle", 0f);
    }

    public void TransitionExit()
    {
    }
}
