using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroHealth : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;

        public event Action HealthHasChanched;

        private State _state;

        public float Max
        {
            get => _state.MaxHealth;
            private set => _state.MaxHealth = value;
        }

        public float Current
        {
            get => _state.CurrentHealth;
            private set
            {
                if (value != _state.CurrentHealth) 
                {
                    _state.CurrentHealth = value;
                    HealthHasChanched?.Invoke();
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;

            if (Current > 0)
                _animator.PlayHit();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = new State(progress.HeroState);
            HealthHasChanched?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.MaxHealth = Max;
            progress.HeroState.CurrentHealth = Current;
        }
    }
}
