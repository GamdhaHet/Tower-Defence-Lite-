using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class ToastMassageView : BaseView
    {
        public Text msgText;
        public float inAnimationTime = 1f;
        public float outAnimationTime = 1;
        public Image bg;
        public Gradient redColorGradient;
        public Gradient whiteColorGradient;

        private Coroutine coroutine;
        private Gradient currentGradient;

        [Button("ShowView")]
        public void ShowView(string msgText1, bool isRedColorText = false, float waitTime = 1.2f)
        {
            if (isRedColorText)
                currentGradient = redColorGradient;
            else
                currentGradient = whiteColorGradient;
            msgText.gameObject.SetActive(true);
            gameObject.SetActive(true);
            msgText.text = msgText1;
            StartCorutine(waitTime);
        }

        private void StartCorutine(float waitTime)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(DoInAnimation(waitTime));
        }

        IEnumerator DoInAnimation(float waitTime)
        {
            float i = 0;
            float time = 1 / inAnimationTime;
            while (i < 1)
            {
                i += Time.deltaTime * time;
                bg.color = whiteColorGradient.Evaluate(i);
                msgText.color = currentGradient.Evaluate(i);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            i = 1;
            time = 1 / outAnimationTime;
            while (i > 0)
            {
                i -= Time.deltaTime * time;
                bg.color = whiteColorGradient.Evaluate(i);
                msgText.color = currentGradient.Evaluate(i);
                yield return null;
            }
            msgText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}