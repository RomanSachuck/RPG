using Assets.CodeBase.Data;
using Assets.CodeBase.Enenemyes;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using Assets.CodeBase.Tools;
using CodeBase.Services.Input;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private float _comboResetInterval;

        [SerializeField] private float _attackDistance;
        [SerializeField] private float _attackOffsetY;

        [SerializeField] private ParticleSystem _hitFx;

        public event Action<EnemyHealth> HeroAttacked;

        private IInputService _inputService;
        private float _comboIntervalCounter;

        private Collider[] _hits = new Collider[3];
        private int _layerMask;

        private float _damageRadius;
        private float _damage;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsMainAttack)
            {
                if (!_animator.IsAttacking)
                    _animator.PlayAttack();

                _animator.SwitchComboOn();
                _comboIntervalCounter = _comboResetInterval;
            }

            if (_comboIntervalCounter > 0)
                _comboIntervalCounter -= Time.deltaTime;
            else
                _animator.SwitchComboOff();
        }

        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                var health = _hits[i].transform.GetComponentInParent<EnemyHealth>();
                health.TakeDamage(_damage);

                _hitFx.Play();

                HeroAttacked?.Invoke(health); 
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _damage = progress.HeroStats.Damage;
            _damageRadius = progress.HeroStats.DamageRadius;
        }

        private int Hit()
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), _damageRadius, _hits, _layerMask);
            PhysicsDebug.DrawSphereDebug(StartPoint(), _damageRadius, 3);

            return hitAmount;
        }

        private Vector3 StartPoint()
        {
            Vector3 point = new Vector3(transform.position.x, transform.position.y + _attackOffsetY, transform.position.z);
            return point + transform.forward * _attackDistance;
        }
    }
}