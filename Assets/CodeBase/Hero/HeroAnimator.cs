using System;
using UnityEngine;

public class HeroAnimator : MonoBehaviour, IAnimationStateReader
{
    [SerializeField] private Animator _animator;

    private readonly int MoveForwardHash = Animator.StringToHash("IsMovingForward");
    private readonly int MoveBackHash = Animator.StringToHash("IsMovingBack");
    private readonly int MoveLeftHash = Animator.StringToHash("IsMovingLeft");
    private readonly int MoveRightHash = Animator.StringToHash("IsMovingRight");
    private readonly int MoveSpeedHash = Animator.StringToHash("Speed");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _moveStateHash = Animator.StringToHash("MoveForward");
    private readonly int _walkBackHash = Animator.StringToHash("WalkBack");
    private readonly int _walkLeftHash = Animator.StringToHash("WalkLeft");
    private readonly int _walkRightHash = Animator.StringToHash("WalkRight");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    public bool IsAttacking => State == AnimatorState.Attack;

    public void ResetToIdle() =>
        OffAllBools();

    public void PlayMoveForward(float velocitySqrMagnitude)
    {
        OffAllBools();
        _animator.SetBool(MoveForwardHash, true);
        _animator.SetFloat(MoveSpeedHash, velocitySqrMagnitude, 0.1f, Time.deltaTime);
    }

    public void PlayMoveBack()
    {
        OffAllBools();
        _animator.SetBool(MoveBackHash, true);
    } 
    
    public void PlayMoveLeft()
    {
        OffAllBools();
        _animator.SetBool(MoveLeftHash, true);
    }

    public void PlayMoveRight()
    {
        OffAllBools();
        _animator.SetBool(MoveRightHash, true);
    }

    public void EnteredState(int stateHash)
    {
        State = StateFor(stateHash);
        StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
        AnimatorState state;
            
        if (CheckMoveState(stateHash))
            state = AnimatorState.Walking;
        else
            state = AnimatorState.Idle;

        return state;
    }

    private bool CheckMoveState(int stateHash)
    {
        return stateHash == _moveStateHash ||
            stateHash == _walkBackHash ||
            stateHash == _walkRightHash ||
            stateHash == _walkLeftHash;
    }

    private void OffAllBools()
    {
        _animator.SetBool(MoveBackHash, false);
        _animator.SetBool(MoveForwardHash, false);
        _animator.SetBool(MoveLeftHash, false);
        _animator.SetBool(MoveRightHash, false);
    }
}