using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState: StatemachineState<EnemyStatemachine, string>,IStatemachineState
{
    GameObject _player;
    NavMeshAgent _agent;
    float _pathTimer;
    Animator _animator;
    public EnemyChaseState(EnemyStatemachine statemachine, NavMeshAgent agent, GameObject player, Animator animator) : base(statemachine)
    {
        _player = player;
        _agent = agent;
        _animator = animator;
    }

    public bool GetTransitionCondition()
    {
        var currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("Spawn_Ground") && currentState.normalizedTime >= .9f)
            return true;

        return false; 
    }

    public void OnAwake()
    {
    }

    public void Tick()
    {
        if (_pathTimer < .2f)
            _pathTimer += Time.deltaTime;
        else
        {
            _pathTimer = 0f;
            _agent.SetDestination(_player.transform.position);
            _agent.transform.LookAt(_statemachine.transform.position + _agent.desiredVelocity);
        }
    }

    public void TransitionEnter()
    {
        _animator.CrossFade("Running_A", 0);
    }

    public void TransitionExit()
    {
    }
}