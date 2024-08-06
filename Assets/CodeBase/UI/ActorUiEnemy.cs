using Assets.CodeBase.Enenemyes;
using Assets.CodeBase.Hero;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class ActorUiEnemy : MonoBehaviour
    {
        [SerializeField] private EnemyHpBar _hpBar;

        private HeroAttack _attack;

        public void Initialize(HeroAttack attack)
        {
            _attack = attack;
            _attack.HeroAttacked += UpdateHpBar;
        }

        private void UpdateHpBar(EnemyHealth health)
        {
            if (!_hpBar.IsActive)
                _hpBar.ShowAndSetName(health.transform.GetComponent<Enemyinfo>().Name);

            _hpBar.SetValue(health.Current, health.Max);

        }
    }
}
