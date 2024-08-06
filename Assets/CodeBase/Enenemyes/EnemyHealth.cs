using DamageNumbersPro;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enenemyes
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Attack _attack;

        public event Action HealthHasChanched;

        public float Max { get; private set; } = 10;
        public float Current { get; private set; } = 10;

        public void TakeDamage(float damage)
        {
            _attack.AbortAttack();

            Current -= damage;
            _animator.PlayHit();
            HealthHasChanched?.Invoke();
        }

    }
}
