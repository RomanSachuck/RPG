using CodeBase.Logic.AnimatorStates;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enenemyes
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int IdleHash = Animator.StringToHash("Idle");
        private readonly int MoveHash = Animator.StringToHash("IsMoving");
        private readonly int AttackHash = Animator.StringToHash("IsAttack");
        private readonly int HitHash = Animator.StringToHash("Hit");
        private readonly int DeathHash = Animator.StringToHash("Death");

        private readonly int IdleStateHash = Animator.StringToHash("Idle");
        private readonly int MoveStateHash = Animator.StringToHash("Move");
        private readonly int AttackStateHash = Animator.StringToHash("Attack");
        private readonly int DeathStateHash = Animator.StringToHash("Death");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void PlayMove() =>
            _animator.SetBool(MoveHash, true);

        public void StopMove() =>
            _animator.SetBool(MoveHash, false);

        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        public void PlayHit() =>
            _animator.SetTrigger(HitHash);

        public void PlayDeath() =>
            _animator.SetTrigger(DeathHash);

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

            if (stateHash == MoveStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == AttackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == DeathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Idle;

            return state;
        }
    }
}
