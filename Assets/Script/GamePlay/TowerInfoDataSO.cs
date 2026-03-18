using TD.TowerHandler;
using UnityEngine;
using Sirenix.OdinInspector;
using TD.RewardSystem;

namespace TD.GamePlay
{
    [CreateAssetMenu(fileName = "TowerInfoDataSO", menuName = "TD/Tower/TowerInfoDataSO ")]
    public class TowerInfoDataSO : SerializedScriptableObject
    {
        public float damage;
        public float range;
        public float fireRate;
        public TowerType towerType;
        public BaseRewardData purchaseCost;
    }
}