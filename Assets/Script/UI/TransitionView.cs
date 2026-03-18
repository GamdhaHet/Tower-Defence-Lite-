using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class TransitionView : BaseView
    {
        [SerializeField] private Image _transitionImage;
        [SerializeField] private float _targetWidthOverride = -1f;

        private Coroutine _transitionRoutine;

        public void SetView(float transitionDuration = 2f, Action onCompleteAction = null)
        {
            base.ShowView();

            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);

            _transitionRoutine = StartCoroutine(WaitAndHide(transitionDuration, onCompleteAction));
        }

        private IEnumerator WaitAndHide(float transitionDuration, Action onCompleteAction)
        {
            float elapsedTime = 0f;
            float duration = transitionDuration;

            yield return null;

            float targetWidth = _targetWidthOverride;
            if (targetWidth <= 0f)
            {
                targetWidth = _transitionImage.rectTransform.rect.width;
                if (targetWidth <= 0f)
                    targetWidth = Mathf.Abs(_transitionImage.rectTransform.sizeDelta.x);
            }

            SetWidth(0f);

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                SetWidth(Mathf.Lerp(0f, targetWidth, t));
                yield return null;
            }

            SetWidth(targetWidth);
            _transitionRoutine = null;

            onCompleteAction?.Invoke();
            HideView();
        }

        private void SetWidth(float width)
        {
            _transitionImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
    }
}