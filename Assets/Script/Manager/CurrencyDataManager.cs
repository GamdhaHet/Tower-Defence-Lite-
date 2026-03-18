using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TD.Manager;
using TD.RewardSystem;
using TD.UI;
using UnityEngine;

public class CurrencyDataManager : SerializedMonoBehaviour
{
    public static CurrencyDataManager Instance;

    [SerializeField] private Dictionary<RewardType, int> _defaultCurrencyData = new();
    [SerializeField, ReadOnly] private Dictionary<RewardType, int> _currentCurrencyData = new();
    [ShowInInspector] private Dictionary<RewardType, List<Action<int, int>>> _onRewardUpdate = new();// int in action represent the old and new value of currency

    private const string CURRENCY_DATA = "currency_data";

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        LoadData();
    }

    [Button]
    public void AddReward(BaseRewardData currencyRewardData)
    {
        if (currencyRewardData != null)
            UpdateCurrencyValue(currencyRewardData.rewardType, currencyRewardData.quantity);
    }

    [Button]
    public void Subtract(BaseRewardData currencyRewardData)
    {
        int currentValue = GetCurrencyQuantity(currencyRewardData.rewardType);
        if (currentValue >= currencyRewardData.quantity)
        {
            UpdateCurrencyValue(currencyRewardData.rewardType, -currencyRewardData.quantity);
            MainUIManager.Instance.GetView<TopBarView>().GetCurrencyViewById(currencyRewardData.rewardType).UpdateData();
        }
    }

    public bool IsEnoughCurrency(BaseRewardData rewardData)
    {
        return GetCurrencyQuantity(rewardData.rewardType) >= rewardData.quantity;
    }

    public int GetCurrencyQuantity(RewardType rewardType)
    {
        return _currentCurrencyData.TryGetValue(rewardType, out int value) ? value : 0;
    }

    private void LoadData()
    {
        _currentCurrencyData = new Dictionary<RewardType, int>();

        string json = PlayerPrefs.GetString(CURRENCY_DATA, string.Empty);
        if (!string.IsNullOrWhiteSpace(json))
        {
            try
            {
                CurrencySaveData saveData = JsonUtility.FromJson<CurrencySaveData>(json);
                if (saveData?.entries != null)
                {
                    for (int i = 0; i < saveData.entries.Count; i++)
                    {
                        CurrencyEntry entry = saveData.entries[i];
                        _currentCurrencyData[entry.rewardType] = entry.quantity;
                    }
                }
            }
            catch (Exception)
            {
                _currentCurrencyData = new Dictionary<RewardType, int>();
            }
        }

        LoadDefaultData();
    }

    private void SaveData()
    {
        CurrencySaveData saveData = new();
        foreach (var kvp in _currentCurrencyData)
        {
            saveData.entries.Add(new CurrencyEntry
            {
                rewardType = kvp.Key,
                quantity = kvp.Value
            });
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(CURRENCY_DATA, json);
        PlayerPrefs.Save();
    }

    private void UpdateCurrencyValue(RewardType rewardType, int delta)
    {
        if (!_currentCurrencyData.TryGetValue(rewardType, out int oldValue))
        {
            oldValue = 0;
            _currentCurrencyData[rewardType] = 0;
        }

        _currentCurrencyData[rewardType] = oldValue + delta;
        SaveData();
        int newValue = _currentCurrencyData[rewardType];
        InvokeActions(rewardType, oldValue, newValue);
        MainUIManager.Instance.GetView<TopBarView>().GetCurrencyViewById(rewardType).UpdateData();
    }

    private void InvokeActions(RewardType rewardType, int oldValue, int newValue)
    {
        if (_onRewardUpdate.ContainsKey(rewardType))
        {
            List<Action<int, int>> actions = new(_onRewardUpdate[rewardType]);
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].Target.Equals(null))
                    _onRewardUpdate[rewardType].Remove(actions[i]);
                else
                    actions[i]?.Invoke(oldValue, newValue);
            }
        }
    }

    private void LoadDefaultData()
    {
        foreach (var value in _defaultCurrencyData)
        {
            if (!_currentCurrencyData.ContainsKey(value.Key))
            {
                _currentCurrencyData.Add(value.Key, value.Value);
            }
        }
        SaveData();
    }

    [Serializable]
    private class CurrencyEntry
    {
        public RewardType rewardType;
        public int quantity;
    }

    [Serializable]
    private class CurrencySaveData
    {
        public List<CurrencyEntry> entries = new();
    }
}