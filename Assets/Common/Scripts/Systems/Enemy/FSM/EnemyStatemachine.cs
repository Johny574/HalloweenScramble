


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatemachine : Statemachine<string>
{

    Enemy _enemy;
    protected override List<StatemachineTrasition<string>> CreateAnyTransitions() => new();

    protected override Dictionary<string, IStatemachineState> CreateStates()
    {
        _enemy = GetComponent<Enemy>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator animator = GetComponentInChildren<Animator>();
        return new()
        {
            {"Spawn", new EnemySpawnState(this, animator)},
            {"Chase", new EnemyChaseState(this, agent, player, animator)},
            {"Despawn", new EnmeyDespawnState(this, animator)},
        };
    }

    protected override List<StatemachineTrasition<string>> CreateTransitions()
    {
        return new()
        {
            new StatemachineTrasition<string>("Spawn", "Chase", States["Chase"].GetTransitionCondition),
            new StatemachineTrasition<string>("Chase", "Despawn", () => _enemy.Dead)
        };
    }
}