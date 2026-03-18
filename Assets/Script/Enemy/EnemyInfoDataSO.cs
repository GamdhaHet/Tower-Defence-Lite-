using Sirenix.OdinInspector;
using TD.RewardSystem;
using UnityEngine;

namespace TD.EnemyHandler
{
    [CreateAssetMenu(fileName = "EnemyInfoDataSO", menuName = "TD/Enemy/EnemyInfoDataSO ")]
    public class EnemyInfoDataSO : SerializedScriptableObject
    {
        public float health;
        public float speed;
        public float damage;
        public BaseRewardData rewardOnDeath;
    }
}