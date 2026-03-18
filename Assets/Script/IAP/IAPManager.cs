using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using TD.Manager;
using TD.UI;
using System.Collections.Generic;

public class IAPManager : MonoBehaviour, IStoreListener, IDetailedStoreListener
{
    public static IAPManager Instance { get; private set; }

    [SerializeField] private IAPProductsSO _iapProductsSO;
    private List<IAPPurchaseData> products = new();

    private IStoreController m_StoreController;
    private IExtensionProvider _extensionProvider;

    private Action _onSuccess;
    private Action _onFail;

    private bool _isAnythingPurchased;
    public bool IsAnythingPurchased { get => _isAnythingPurchased; private set => _isAnythingPurchased = value; }

    private void Awake()
    {
        Instance = this;
        Init();
    }

    public void Init(Action onComplete = null)
    {
        StartCoroutine(InitCoroutine());
    }

    public void PurchaseProduct(string productId, Action onSuccess, Action onFail)
    {
        MainUIManager.Instance.ShowView<BufferingScreen>();

        InternetChecker.Instance.CheckInternet(hasInternet =>
        {
            if (!hasInternet)
            {
                MainUIManager.Instance.HideView<BufferingScreen>();
                ShowFailPopup();
                onFail?.Invoke();
                return;
            }

            _onSuccess = onSuccess;
            _onFail = onFail;

            BuyProduct(productId);
        });
    }

    public string GetPrice(string productId)
    {
        if (!IsInitialized()) return GetDefaultPrice(productId);

        var meta = m_StoreController.products.WithID(productId)?.metadata;
        return meta == null ? GetDefaultPrice(productId) : meta.localizedPriceString;
    }

    public bool IsProductPurchased(string productId)
    {
        if (!IsInitialized()) return false;
        var product = m_StoreController.products.WithID(productId);
        return product != null && product.hasReceipt;
    }

    public void RestorePurchases()
    {
        if (!IsInitialized()) return;

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            var apple = _extensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result, msg) => { });
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        _extensionProvider = extensions;
        IsAnythingPurchased = HasAnyReceipt();
    }

    public void OnInitializeFailed(InitializationFailureReason error) { }

    public void OnInitializeFailed(InitializationFailureReason error, string message) { }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        _onSuccess?.Invoke();
        IsAnythingPurchased = true;
        MainUIManager.Instance.HideView<BufferingScreen>();

#if PLAYFAB
            LoginManager.Instance.CheckForLoginOrCreateNewAccount();
#endif
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription desc)
        => HandlePurchaseFailed();

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        => HandlePurchaseFailed();

    private bool IsInitialized() => m_StoreController != null && _extensionProvider != null;

    private void BuyProduct(string productId)
    {
        if (!IsInitialized()) { HandlePurchaseFailed(); return; }

        var product = m_StoreController.products.WithID(productId);
        if (product == null || !product.availableToPurchase) { HandlePurchaseFailed(); return; }

        m_StoreController.InitiatePurchase(product);
    }

    private void HandlePurchaseFailed()
    {
        _onFail?.Invoke();
        MainUIManager.Instance.HideView<BufferingScreen>();
        ShowFailPopup();
    }

    private void ShowFailPopup()
        => MainUIManager.Instance.GetView<ToastMassageView>().ShowView("Purchase Failed");

    private bool HasAnyReceipt()
    {
        if (!IsInitialized()) return false;
        foreach (var data in _iapProductsSO.GetAllProducts())
        {
            var p = m_StoreController.products.WithID(data.packId);
            if (p != null && p.hasReceipt) return true;
        }
        return false;
    }

    private string GetDefaultPrice(string productId)
    {
        foreach (var data in _iapProductsSO.GetAllProducts())
            if (data.packId == productId) return data.defaultPriceText;
        return "$0.00";
    }

    private IEnumerator InitCoroutine()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }

        yield return null;
    }

    private void InitializePurchasing()
    {
        if (IsInitialized()) return;

        products = _iapProductsSO.GetAllProducts();
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        for (int i = 0; i < products.Count; i++)
        {
            builder.AddProduct(products[i].packId, products[i].productType);
        }

        UnityPurchasing.Initialize(this, builder);
        StartCoroutine(WaitForInternetCheckBoughtProducts());
    }

    private IEnumerator WaitForInternetCheckBoughtProducts()
    {
        while (!IsInitialized())
        {
            yield return new WaitForSeconds(1.0f);
        }

        for (int i = 0; i < products.Count; i++)
        {
            Product product = m_StoreController.products.WithID(products[i].packId);
            if (product != null && product.hasReceipt)
            {
                IsAnythingPurchased = true;
                break;
            }
        }
    }
}