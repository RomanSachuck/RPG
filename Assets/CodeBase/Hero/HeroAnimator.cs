using CodeBase.Logic.AnimatorStates;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _moveForwardHash = Animator.StringToHash("IsMovingForward");
        private readonly int _moveBackHash = Animator.StringToHash("IsMovingBack");
        private readonly int _moveSpeedHash = Animator.StringToHash("Speed");
        private readonly int _jumpStartHash = Animator.StringToHash("JumpStart");
        private readonly int _jumpEndHash = Animator.StringToHash("JumpEnd");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _moveStateHash = Animator.StringToHash("MoveForward");
        private readonly int _moveBackStateHash = Animator.StringToHash("MoveBack");
        private readonly int _jumpStartStateHash = Animator.StringToHash("JumpStart");
        private readonly int _jumpAirStateHash = Animator.StringToHash("JumpAir");
        private readonly int _jumpEndStateHash = Animator.StringToHash("JumpEnd");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public bool IsAttacking => State == AnimatorState.Attack;

        public void ResetToIdle() =>
            OffAllBools();

        public void PlayMoveForward(float velocityMagnitude)
        {
            OffAllBools();
            _animator.SetBool(_moveForwardHash, true);
            _animator.SetFloat(_moveSpeedHash, velocityMagnitude, 0.1f, Time.deltaTime);
        }

        public void PlayMoveBack()
        {
            OffAllBools();
            _animator.SetBool(_moveBackHash, true);
        }

        public void PlayJumpStart() =>
            _animator.SetTrigger(_jumpStartHash);

        public void PlayJumpEnd() =>
            _animator.SetTrigger(_jumpEndHash);

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
            else if (CheckJumpState(stateHash))
                state = AnimatorState.Jumping;
            else
                state = AnimatorState.Idle;

            return state;
        }

        private bool CheckMoveState(int stateHash)
        {
            return stateHash == _moveStateHash ||
                stateHash == _moveBackStateHash;
        }

        private bool CheckJumpState(int stateHash)
        {
            return stateHash == _jumpStartStateHash ||
                stateHash == _jumpAirStateHash;
        }

        private void OffAllBools()
        {
            _animator.SetBool(_moveBackHash, false);
            _animator.SetBool(_moveForwardHash, false);
        }
    }
}