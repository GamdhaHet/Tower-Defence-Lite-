using UnityEngine;

namespace TD.EnemyHandler
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;
        public AllEnemyInfoDataSO enemyInfoDataSO;

        private void Awake()
        {
            Instance = this;
        }

        public Enemy GetEnemy(EnemyType enemyType)
        {
            return enemyInfoDataSO.GetEnemy(enemyType);
        }
    }
}