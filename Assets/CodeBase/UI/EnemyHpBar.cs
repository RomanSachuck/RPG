using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.UI
{
    public class EnemyHpBar : MonoBehaviour
    {
        public bool IsActive {  get; private set; }

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _enemyNameText;
        [SerializeField] private Image _fillImageBar;

        [SerializeField] private float _hideCooldown;

        private float _hideCooldownCounter;
        private Coroutine _currentCoroutine;

        private void Awake() => 
            _hideCooldownCounter = _hideCooldown;

        private void Update()
        {
            if (_hideCooldownCounter > 0)
                _hideCooldownCounter -= Time.deltaTime;
            else if(IsActive)
                Hide();
        }

        public void ShowAndSetName(string enemyName)
        {
            IsActive = true;
            _hideCooldownCounter = _hideCooldown;

            _enemyNameText.text = enemyName;

            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ShowBar());
        }

        public void SetValue(float current, float max)
        {
            _hideCooldownCounter = _hideCooldown;

            _fillImageBar.fillAmount = current / max;
        }

        public void Hide()
        {
            IsActive = false;

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(HideBar());
        }

        private IEnumerator ShowBar()
        {
            while(_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            _canvasGroup.alpha = 1;
        }

        private IEnumerator HideBar()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            _canvasGroup.alpha = 0;
        }
    }
}
