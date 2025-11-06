



using System.Collections.Generic;
using UnityEngine;

public class PlayerStatemachine : Statemachine<string>
{
    Movement move;
    protected override List<StatemachineTrasition<string>> CreateAnyTransitions()
    {
        return new();
    }

    protected override Dictionary<string, IStatemachineState> CreateStates()
    {
        move = GetComponent<Movement>();
        Animator animator = GetComponentInChildren<Animator>();
        return new()
        {
            {"Idle", new PlayerIdleState(this, move, animator)},
            {"Move", new PlayerMoveState(this, animator)},
            {"JumpStart", new PlayerJumpStartState(this, animator, move)},
            {"Jump", new PlayerJumpState(this, animator)}
        };
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions()
    {
        return new()
        {
             new StatemachineTrasition<string>("Idle", "Move", () => !States["Idle"].GetTransitionCondition()),
             new StatemachineTrasition<string>("Idle", "JumpStart", States["JumpStart"].GetTransitionCondition),

             new StatemachineTrasition<string>("Move", "Idle", States["Idle"].GetTransitionCondition),
             new StatemachineTrasition<string>("Move", "JumpStart",  States["JumpStart"].GetTransitionCondition),

             new StatemachineTrasition<string>("JumpStart", "Jump", () => States["Jump"].GetTransitionCondition()),

             new StatemachineTrasition<string>("Jump", "Idle", () => !States["JumpStart"].GetTransitionCondition()),
             new StatemachineTrasition<string>("Jump", "Move", () => !States["JumpStart"].GetTransitionCondition()),


        };
    }
}