using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private float _fillSpeed;

        private float _currentFill;
        private float _targetFill;
        private Action _endAction;
        private Coroutine _fillCoroutine;

        public void SetEndEction(Action action)
        {
            _endAction = action;
        }

        public void SetFillValue(float value)
        {
            _targetFill = value;
            _fill.fillAmount = value;
        }

        public void Fill(float percentage)
        {
            _targetFill = percentage / 100;
            StartFill();
        }

        private void StartFill()
        {
            _fillCoroutine ??= StartCoroutine(SimulateFill());
        }

        private IEnumerator SimulateFill()
        {
            while (_currentFill < _targetFill)
            {
                _currentFill += Time.deltaTime * _fillSpeed;
                _currentFill = Mathf.Clamp(_currentFill, 0, 1);
                _fill.fillAmount = _currentFill;
                yield return null;
            }

            if (_currentFill >= 1)
                _endAction?.Invoke();

            _fillCoroutine = null;
        }

    }
}
