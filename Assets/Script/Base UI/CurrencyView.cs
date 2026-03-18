using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] protected RewardType _rewardType;
    [SerializeField] protected Image _currencyImage;
    [SerializeField] protected Text _currencyText;
    [SerializeField] private Transform _targertTransform;
    private Coroutine _coroutine;
    private Tweener _tweener;

    public void SetCurrencyView()
    {
        _currencyImage.sprite = ItemSpriteHandler.Instance.GetCurrencySprite(_rewardType);
        _currencyText.text = CurrencyDataManager.Instance.GetCurrencyQuantity(_rewardType).ToString();
    }

    public Transform GetTargetTransform()
    {
        return _targertTransform;
    }

    public virtual void UpdateData()
    {
        StopCo();
        int newData = CurrencyDataManager.Instance.GetCurrencyQuantity(_rewardType);
        int oldData = Convert.ToInt32(_currencyText.text);
        if (gameObject.activeInHierarchy)
            _coroutine = StartCoroutine(DoTextAnimation(oldData, newData));
        PlayWholeAnimation();
    }

    private void StopCo()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void PlayWholeAnimation()
    {
         _tweener = _currencyImage.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack).Play().OnComplete(() =>
         {
             _tweener = _currencyImage.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack).Play().OnComplete(() =>
             {
                 _tweener.Kill();
                 _tweener = null;
             });
         });
    }

    IEnumerator DoTextAnimation(int startValue, int endValue)
    {
        float i = 0;
        float rate = 1 / 0.3f;
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            _currencyText.text = ((int)Mathf.Lerp(startValue, endValue, i)).ToString();
            yield return null;
        }
    }
}