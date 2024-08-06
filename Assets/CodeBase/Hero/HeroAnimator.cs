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
        private readonly int _attackHash = Animator.StringToHash("Attack");
        private readonly int _comboHash = Animator.StringToHash("Combo");
        private readonly int _moveSpeedHash = Animator.StringToHash("Speed");
        private readonly int _jumpStartHash = Animator.StringToHash("JumpStart");
        private readonly int _jumpEndHash = Animator.StringToHash("JumpEnd");
        private readonly int _hitHash = Animator.StringToHash("Hit");
        private readonly int _dieHash = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _moveForwardStateHash = Animator.StringToHash("MoveForward");
        private readonly int _moveBackStateHash = Animator.StringToHash("MoveBack");
        private readonly int _jumpAttackStateHash = Animator.StringToHash("JumpAttack");
        private readonly int _baseAttackStateHash = Animator.StringToHash("AttackBase");
        private readonly int _combo01AttackStateHash = Animator.StringToHash("Combo01");
        private readonly int _combo02AttackStateHash = Animator.StringToHash("Combo02");
        private readonly int _combo03AttackStateHash = Animator.StringToHash("Combo03");
        private readonly int _combo04AttackStateHash = Animator.StringToHash("Combo04");
        private readonly int _combo05AttackStateHash = Animator.StringToHash("Combo05");
        private readonly int _jumpStartStateHash = Animator.StringToHash("JumpStart");
        private readonly int _jumpAirStateHash = Animator.StringToHash("JumpAir");
        private readonly int _jumpEndStateHash = Animator.StringToHash("JumpEnd");
        private readonly int _dieStateHash = Animator.StringToHash("Die");

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

        public void PlayAttack() => 
            _animator.SetTrigger(_attackHash);

        public void SwitchComboOn() =>
            _animator.SetBool(_comboHash, true);

        public void SwitchComboOff() =>
            _animator.SetBool(_comboHash, false);

        public void PlayJumpStart() =>
            _animator.SetTrigger(_jumpStartHash);

        public void PlayJumpEnd() =>
            _animator.SetTrigger(_jumpEndHash);

        public void PlayHit() =>
            _animator.SetTrigger(_hitHash);

        public void PlayDeath() =>
            _animator.SetTrigger(_dieHash);

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
            else if (stateHash == _dieStateHash)
                state = AnimatorState.Died;
            else if (CheckAttackState(stateHash))
                state = AnimatorState.Attack;
            else
                state = AnimatorState.Idle;

            return state;
        }

        private bool CheckAttackState(int stateHash)
        {
            return stateHash == _baseAttackStateHash || 
                stateHash == _jumpAttackStateHash ||
                stateHash == _combo01AttackStateHash ||
                stateHash == _combo02AttackStateHash ||
                stateHash == _combo03AttackStateHash ||
                stateHash == _combo04AttackStateHash ||
                stateHash == _combo05AttackStateHash;
        }

        private bool CheckMoveState(int stateHash)
        {
            return stateHash == _moveForwardStateHash ||
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