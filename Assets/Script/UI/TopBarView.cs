using System.Collections.Generic;
using TD.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class TopBarView : BaseView
    {
        [SerializeField] private Dictionary<RewardType, CurrencyView> _currencyViewsDict = new();
        [SerializeField] private HeartView _heartView;

        public override void ShowView()
        {
            foreach (var currencyView in _currencyViewsDict)
            {
                currencyView.Value.SetCurrencyView();
            }
            base.ShowView();
        }

        public CurrencyView GetCurrencyViewById(RewardType rewardType)
        {
            if (_currencyViewsDict.ContainsKey(rewardType))
                return _currencyViewsDict[rewardType];
            return _currencyViewsDict[RewardType.COIN];
        }
        
        public void SetHeartView(int lives)
        {
            _heartView.SetHeartView(lives);
        }

        public void OnPauseButtonClick()
        {
            MainUIManager.Instance.ShowView<PauseMenuView>();
        }
    }
}
