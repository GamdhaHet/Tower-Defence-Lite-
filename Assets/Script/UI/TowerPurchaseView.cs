using UnityEngine;
using TD.RewardSystem;
using TD.GamePlay;
using TD.Manager;
using TD.UI;

public class TowerPurchaseView : MonoBehaviour
{
    [SerializeField] private RewardView _rewardView;
    private BaseRewardData _baseRewardData;

    public void SetView(BaseRewardData purchaseCost)
    {
        _baseRewardData = purchaseCost;
        _rewardView.SetReward(purchaseCost);
        gameObject.SetActive(!TowerManager.Instance.IsDualShotTowerPurchased);
    }

    public void OnDualShotTowerPurchase()
    {
        if (CurrencyDataManager.Instance.IsEnoughCurrency(_baseRewardData))
        {
            TowerManager.Instance.OnDualShotTowerPurchased();
            gameObject.SetActive(false);
        }
        else
            MainUIManager.Instance.GetView<ToastMassageView>().ShowView("Not enough money to purchase the tower!", true);
    }
}
