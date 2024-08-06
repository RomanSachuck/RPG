using Assets.CodeBase.Enenemyes;
using CodeBase;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(EnemyAnimator))]
public class AnimateAlongAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimator _animator;

    private void Update()
    {
        if (ShouldMove())
            _animator.PlayMove();
        else
            _animator.StopMove();
    }

    private bool ShouldMove() => 
        _agent.velocity.sqrMagnitude >= Consts.Epsilon;
}
