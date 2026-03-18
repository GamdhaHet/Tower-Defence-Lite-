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
}