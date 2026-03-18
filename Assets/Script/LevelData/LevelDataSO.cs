using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TD.RewardSystem;

namespace TD.LevelData
{
    [CreateAssetMenu(fileName = "LevelDataSO", menuName = "TD/Level/LevelDataSO ")]
    public class LevelDataSO : SerializedScriptableObject
    {
        public List<EnemyWaveData> enemyWaveData;
        public List<BaseRewardData> levelWinRewards;

        public float GetHealthMultiplier(int waveIndex)
        {
            return enemyWaveData[waveIndex].healthMultiplier;
        }

        public int GetTotalEnemiesInWave(int waveIndex)
        {
            int totalEnemiesInWave = 0;
            foreach (EnemySpawnData enemySpawn in enemyWaveData[waveIndex].enemySpawnData)
                totalEnemiesInWave += enemySpawn.enemyCount;
            return totalEnemiesInWave;
        }

        public Dictionary<int, Dictionary<EnemyType, int>> GetEnemiesToSpawn()
        {
            Dictionary<int, Dictionary<EnemyType, int>> totalEnemyToSpawn = new();
            for (int i = 0; i < enemyWaveData.Count; i++)
            {
                EnemyWaveData waveData = enemyWaveData[i];
                Dictionary<EnemyType, int> enemyToSpawn = new();
                foreach (EnemySpawnData enemySpawn in waveData.enemySpawnData)
                    enemyToSpawn[enemySpawn.enemies] = enemySpawn.enemyCount;
                totalEnemyToSpawn.Add(i, enemyToSpawn);
            }
            return totalEnemyToSpawn;
        }
    }

    [System.Serializable]
    public class EnemyWaveData
    {
        public List<EnemySpawnData> enemySpawnData;
        public float healthMultiplier;
    }

    [System.Serializable]
    public class EnemySpawnData
    {
        public EnemyType enemies;
        public int enemyCount;
    }
}