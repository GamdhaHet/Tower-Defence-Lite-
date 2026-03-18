using System.Collections.Generic;
using TD.EnemyHandler;
using TD.EnemyMovement;
using TD.Manager;
using UnityEngine;

namespace TD.EnemyHandler
{
    public class EnemyMovementHandler : MonoBehaviour
    {
        public static EnemyMovementHandler Instance;
        public List<EnemyMovementData> enemyMovementDatas;

        private void Awake()
        {
            Instance = this;
        }

        public void OnEnable()
        {
            GameManager.onLevelExit += DestroyAllEnemies;
        }

        public void OnDisable()
        {
            GameManager.onLevelExit -= DestroyAllEnemies;
        }

        private void DestroyAllEnemies()
        {
            List<EnemyMovementData> snapshot = new(enemyMovementDatas);
            enemyMovementDatas.Clear();

            for (int i = 0; i < snapshot.Count; i++)
            {
                EnemyMovementData enemyData = snapshot[i];
                enemyData.enemy.EnemyDie();
            }
        }

        public void AddEnemy(Enemy enemy)
        {
            EnemyMovementData enemyMovementData = enemyMovementDatas.Find(data => data.enemy == enemy);
            if (enemyMovementData != null)
                return;

            enemyMovementDatas.Add(new EnemyMovementData(enemy));
        }

        public void RemoveEnemy(Enemy enemy)
        {
            EnemyMovementData enemyMovementData = enemyMovementDatas.Find(data => data.enemy == enemy);
            if (enemyMovementData != null)
                enemyMovementDatas.Remove(enemyMovementData);
        }

        private void Update()
        {
            if (enemyMovementDatas.Count == 0)
                return;

            for (int i = 0; i < enemyMovementDatas.Count; i++)
            {
                EnemyMovementData enemyData = enemyMovementDatas[i];
                if (enemyData.enemy != null && !enemyData.enemy.isDead)
                {
                    Vector3 dir = enemyData.target.CheckpointTransform.position - enemyData.enemy.transform.localPosition;
                    enemyData.enemy.transform.Translate(dir.normalized * enemyData.enemy.speed * Time.deltaTime, Space.World);

                    if (Vector3.Distance(enemyData.enemy.transform.position, enemyData.target.CheckpointTransform.position) <= 0.4f)
                        GetNextWaypoint(enemyData);
                }
            }
        }

        private void GetNextWaypoint(EnemyMovementData enemyData)
        {
            if (enemyData.wavepointIndex >= CheckpointsManager.Instance.Checkpoints.Count - 1)
            {
                OnReachToPlayerBase(enemyData.enemy);
                return;
            }

            enemyData.wavepointIndex++;
            enemyData.target = CheckpointsManager.Instance.Checkpoints[enemyData.wavepointIndex];
        }

        private void OnReachToPlayerBase(Enemy enemy)
        {
            GameManager.Instance.playerBase.PlayHitEffect(enemy.damage);
            EnemyWaveManager.Instance.OnEnemyDie();
            enemy.AfterDie();
        }
    }
}

[System.Serializable]
public class EnemyMovementData
{
    public Enemy enemy;
    public Checkpoint target;
    public int wavepointIndex = 0;

    public EnemyMovementData(Enemy enemy)
    {
        this.enemy = enemy;
        this.target = CheckpointsManager.Instance.defaultCheckpoint;
        this.wavepointIndex = 0;
    }
}