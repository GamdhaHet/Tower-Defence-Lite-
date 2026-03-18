using System.Collections.Generic;
using Sirenix.OdinInspector;
using TD.Manager;
using TD.RewardSystem;
using TD.UI;
using UnityEngine;

public class ShopManager : SerializedMonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private List<ShopDealData> _bestShopDeals;
    [SerializeField] private BaseRewardData _dualShotTowerPurchaseCost;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MainUIManager.Instance.GetView<ShopView>().SetBestShopDeal(_bestShopDeals);
        MainUIManager.Instance.GetView<ShopView>().SetTowerPurchaseView(_dualShotTowerPurchaseCost);
    }
}

public class ShopDealData
{
    public IAPProductDataSO iAPProductDataSO;
    public BaseRewardData reward;
}