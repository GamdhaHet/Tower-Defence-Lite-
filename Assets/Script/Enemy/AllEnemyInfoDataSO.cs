using System.Collections.Generic;
using TD.EnemyHandler;
using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemyInfoDataSO", menuName = "TD/Enemy/AllEnemyInfoDataSO ")]
public class AllEnemyInfoDataSO : ScriptableObject
{
    public List<EnemyInfoData> enemyInfoData;

    public EnemyInfoData GetEnemyInfoData(EnemyType enemyType)
    {
        return enemyInfoData.Find(data => data.enemyType == enemyType);
    }

    public Enemy GetEnemy(EnemyType enemyType)
    {
        return GetEnemyInfoData(enemyType).enemyPrefab;
    }
}

[System.Serializable]
public class EnemyInfoData
{
    public EnemyType enemyType;
    public Enemy enemyPrefab;
}