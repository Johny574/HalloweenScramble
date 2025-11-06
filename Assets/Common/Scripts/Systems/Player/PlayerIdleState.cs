



using UnityEngine;

public class PlayerIdleState : StatemachineState<PlayerStatemachine, string>, IStatemachineState
{
    Movement _movement;
    Animator _animator;
    public PlayerIdleState(PlayerStatemachine statemachine, Movement movement, Animator animator) : base(statemachine)
    {
        _animator = animator;
        _movement = movement;
    }

    public bool GetTransitionCondition()
    {
        return _movement.FrameInput == Vector2.zero;
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Idle_A", 0f);
    }

    public void TransitionExit()
    {
    }
}