using System;
using UnityEngine;

namespace Assets.CodeBase.Enenemyes
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Aggro _aggro;
        [SerializeField] private FollowAbstract _moveToPlayer;
        [SerializeField] private FollowAbstract _moveToRandom;
        [SerializeField] private Collider _hittableCollider;

        public event Action EnemyDied;

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

            EnemyDied?.Invoke();
        }

        private void DisableLifeComponents()
        {
            _aggro.enabled = false;
            _moveToPlayer.enabled = false;
            _moveToRandom.enabled = false;
            _hittableCollider.enabled = false;
        }
    }
}
