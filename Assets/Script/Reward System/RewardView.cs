using UnityEngine;
using UnityEngine.UI;

namespace TD.RewardSystem
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private Text _rewardAmountText;
        [SerializeField] private Image _rewardImage;

        public void SetReward(BaseRewardData reward)
        {
            _rewardAmountText.text = reward.GetQuantity().ToString();
            _rewardImage.sprite = reward.GetSprite();
        }
    }
}