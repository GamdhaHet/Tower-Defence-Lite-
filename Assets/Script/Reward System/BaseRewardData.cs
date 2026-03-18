using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD.RewardSystem
{
    public class BaseRewardData : BaseReward
    {
        public RewardType rewardType;
        public int quantity;

        public override bool TryToAddReward(BaseReward baseReward)
        {
            if (baseReward is BaseRewardData currencyRewardData)
            {
                if (currencyRewardData.rewardType == rewardType)
                {
                    quantity += currencyRewardData.quantity;
                    return true;
                }
            }
            return false;
        }

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

        public override void PlayAnimation(Transform itemTransform, Action endAnimationAction = null)
        {
            // switch (rewardType)
            // {
            //     case RewardType.COIN:
            //     case RewardType.GEMS:
            //         Action onStartAnimation = MainUIManager.Instance.GetView<TopBarView>().GetCurrencyViewById(rewardType).UpdateData;
            //         CollectAnimationManager.Instance.PlayCurrencyCollectAnimation(this, itemTransform.position, endAnimationAction, onStartAnimation);
            //         break;
            //     case RewardType.MAGNIFY_GLASS:
            //     case RewardType.MAGNIFY_COMPASS:
            //         CollectAnimationManager.Instance.PlayCollectBoosterFromChestRewardAnimation(rewardType, itemTransform.position, quantity, 0.5f, endAnimationAction);
            //         break;
            //     default:
            //         endAnimationAction?.Invoke();
            //         break;
            // }
        }

        public override Dictionary<Sprite, int> GetRewardData()
        {
            return new Dictionary<Sprite, int> { { GetSprite(), GetQuantity() } };
        }
    }
}