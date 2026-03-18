using System.Collections.Generic;
using TD.RewardSystem;
using UnityEngine;

namespace TD.UI
{
    public class ShopView : BaseView
    {
        [SerializeField] private List<BestShopDeal> _bestShopDeals;
        [SerializeField] private TowerPurchaseView _towerPurchaseView;

        public void SetBestShopDeal(List<ShopDealData> shopDealDatas)
        {
            for (int i = 0; i < shopDealDatas.Count; i++)
            {
                _bestShopDeals[i].SetReward(shopDealDatas[i]);
            }
        }

        public void SetTowerPurchaseView(BaseRewardData purchaseCost)
        {
            _towerPurchaseView.SetView(purchaseCost);
        }
    }
}