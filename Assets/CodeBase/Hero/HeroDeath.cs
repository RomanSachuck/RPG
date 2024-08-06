using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;

        public event Action HeroDied;

        private bool _isDead;

        private void OnEnable() =>
            _health.HealthHasChanched += HealthChanched;

        private void OnDisable() =>
            _health.HealthHasChanched -= HealthChanched;

        private void HealthChanched()
        {
            if (_health.Current <= 0 && !_isDead)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _animator.PlayDeath();

            DisableLifeComponents();

            HeroDied?.Invoke();
        }

        private void DisableLifeComponents()
        {
            _move.enabled = false;
            _attack.enabled = false;
        }
    }
}
