using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI
{
    public class HeroHpBar : MonoBehaviour
    {
        [SerializeField] private Image[] _hearts;
        public void SetValue(float current, float max)
        {
            float fullHeartValue = max / _hearts.Length;

            ClearAllHearts();

            for (int i = 0; i < _hearts.Length; i++)
            {
                if (current > 0)
                {
                    _hearts[i].fillAmount = current / fullHeartValue;
                    current -= fullHeartValue;
                }
                else break;
            }
        }

        private void ClearAllHearts()
        {
            for (int i = 0; i < _hearts.Length; i++)
                _hearts[i].fillAmount = 0;
        }
    }
}
