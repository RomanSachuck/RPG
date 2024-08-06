using Assets.CodeBase.Hero;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Services.Factory;
using Assets.CodeBase.Tools;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Enenemyes
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private ParticleSystem _attackParticle;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _attackDistance;

        [SerializeField] private float _damageRadius;
        [SerializeField] private float _offsetY;

        private Collider[] _hits = new Collider[1];
        private int _layerMask;

        private Transform _heroTransform;
        private HeroHealth _heroHealth;

        private float _currentCooldown;
        private bool _isAttacking;
        private bool _heroIsDead;

        private void Awake()
        {
            var factory = AllServices.Container.Single<IGameFactory>();
            factory.HeroCrated += () =>
            { _heroTransform = factory.HeroGameObject.transform;
                _heroTransform.GetComponent<HeroDeath>().HeroDied += HeroDied;
            };

            _currentCooldown = _cooldown;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if(Hit(out Collider hit))
            {
                PhysicsDebug.DrawSphereDebug(StartPoint(), _damageRadius, 1);

                if(_heroHealth == null)
                    _heroHealth = _heroTransform.GetComponent<HeroHealth>();

                _heroHealth.TakeDamage(_damage);

                _attackParticle.Play();
            }
        }

        private void OnAttackEnded()
        {
            _currentCooldown = _cooldown;
            _isAttacking = false;
        }

        public void AbortAttack() =>
            _isAttacking = false;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform.position);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool Hit(out Collider hit)
        {
            Vector3 startpoint = StartPoint();
            int hitsCount = Physics.OverlapSphereNonAlloc(startpoint, _damageRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            Vector3 point = new Vector3(transform.position.x, transform.position.y + _offsetY, transform.position.z);
            return point + transform.forward * _attackDistance;
        }

        private void HeroDied() =>
            _heroIsDead = true;

        private bool CanAttack() =>
            !_isAttacking && CooldownIsUp() 
            && HeroReached() && !_heroIsDead;

        private bool HeroReached() => 
            Vector3.Distance(transform.position, _heroTransform.position) <= _attackDistance;

        private void UpdateCooldown()
        {
            if(!CooldownIsUp())
            _currentCooldown -= Time.deltaTime;
        }

        private bool CooldownIsUp() =>
            _currentCooldown <= 0;
    }
}
