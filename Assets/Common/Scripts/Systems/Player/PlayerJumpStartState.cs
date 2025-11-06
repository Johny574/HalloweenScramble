

using UnityEngine;

public class PlayerJumpStartState: StatemachineState<PlayerStatemachine, string>, IStatemachineState
{
    Animator _animator;
    Movement _move;
    public PlayerJumpStartState(PlayerStatemachine statemachine, Animator animator, Movement move) : base(statemachine)
    {
        _animator = animator;
        _move = move;
    }

    public bool GetTransitionCondition()
    {
        return !_move.Grounded && _move.Jumping;
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Jump_Start", 0f);
    }

    public void TransitionExit()
    {
    }
}