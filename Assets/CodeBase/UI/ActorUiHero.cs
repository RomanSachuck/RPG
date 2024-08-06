using Assets.CodeBase.Hero;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class ActorUiHero : MonoBehaviour
    {
        [SerializeField] private HeroHpBar _hpBar;

        private HeroHealth _health;

        public void Initialize(HeroHealth health)
        {
            _health = health;
            _health.HealthHasChanched += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.Current, _health.Max);
    }
}
