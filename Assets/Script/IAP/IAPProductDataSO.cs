using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(fileName = "IAPProductDataSO", menuName = "TD/ShopManager/Purchase/IAPProductDataSO")]
public class IAPProductDataSO : ScriptableObject
{
    public IAPPurchaseData IAPPurchaseData;
}

[System.Serializable]
public class IAPPurchaseData
{
    public string defaultPriceText;
    public ProductType productType;
    public string packId;

    public bool TryToGetDefaultPrice(string packId, out string defaultPrice)
    {
        defaultPrice = string.Empty;
        if (packId == this.packId)
        {
            defaultPrice = defaultPriceText;
            return true;
        }
        return false;
    }
}