using UnityEngine;

public class ItemSpriteHandler : MonoBehaviour
{
    public static ItemSpriteHandler Instance;

    [SerializeField] private CurrencySpriteDataSO currencySpriteDataSO;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetCurrencySprite(RewardType rewardType)
    {
        if (currencySpriteDataSO.TryToGetSprite(rewardType, out Sprite sprite))
            return sprite;
        return null;
    }
}