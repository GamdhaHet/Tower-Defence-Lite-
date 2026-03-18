using UnityEngine;

namespace TD.RewardSystem
{
    public class BaseRewardData : BaseReward
    {
        public RewardType rewardType;
        public int quantity;

        public override int GetQuantity()
        {
            return quantity;
        }

        public override Sprite GetSprite()
        {
            return ItemSpriteHandler.Instance.GetCurrencySprite(rewardType);
        }

        public override void GiveReward()
        {
            CurrencyDataManager.Instance.AddReward(this);
        }

    }
}