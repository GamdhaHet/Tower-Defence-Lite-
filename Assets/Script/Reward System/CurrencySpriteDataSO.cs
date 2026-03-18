using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencySpriteDataSO", menuName = "TD/SpriteDataSO/CurrencySpriteDataSO")]
public class CurrencySpriteDataSO : SerializedScriptableObject
{
    public Dictionary<RewardType, Sprite> currencySpriteData = new Dictionary<RewardType, Sprite>();

    public bool TryToGetSprite(RewardType rewardType, out Sprite sprite)
    {
        return currencySpriteData.TryGetValue(rewardType, out sprite);
    }
}