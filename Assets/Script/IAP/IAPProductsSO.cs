using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IAPProductsSO", menuName = "TD/ShopManager/Purchase/IAPProductsSO")]
public class IAPProductsSO : ScriptableObject
{
    [SerializeField] private List<IAPProductDataSO> productDatas;

    public List<IAPPurchaseData> GetAllProducts()
    {
        List<IAPPurchaseData> products = new();
        foreach (var product in productDatas)
            products.Add(product.IAPPurchaseData);
        return products;
    }
}