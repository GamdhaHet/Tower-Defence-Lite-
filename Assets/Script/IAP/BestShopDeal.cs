using Sirenix.OdinInspector;
using TD.Manager;
using TD.UI;
using UnityEngine;
using UnityEngine.UI;

public class BestShopDeal : MonoBehaviour
{
    [SerializeField] private Image _rewardImage;
    [SerializeField] private Text _rewardAmountText;
    [SerializeField] private Text _priceText;
    [ShowInInspector] private ShopDealData _shopDealData;

    public void SetReward(ShopDealData shopDealData)
    {
        _shopDealData = shopDealData;
        _priceText.text = shopDealData.iAPProductDataSO.IAPPurchaseData.defaultPriceText;
        _rewardAmountText.text = _shopDealData.reward.GetQuantity().ToString();
        _rewardImage.sprite = _shopDealData.reward.GetSprite();
        gameObject.SetActive(true);
    }

    public void OnPurchaseButtonClicked()
    {
        IAPManager.Instance.PurchaseProduct(_shopDealData.iAPProductDataSO.IAPPurchaseData.packId, OnPurchaseSuccess, OnPurchaseFail);
    }

    private void OnPurchaseSuccess()
    {
        _shopDealData.reward.GiveReward();
    }

    private void OnPurchaseFail()
    {
        MainUIManager.Instance.GetView<ToastMassageView>().ShowView("Purchase failed");
    }
}
