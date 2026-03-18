using Sirenix.OdinInspector;
using TD.Pool;
using UnityEngine;

namespace TD.EnemyHandler
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyInfoDataSO _enemyInfoDataSO;
        [ShowInInspector] public float health { get; set; }
        public float speed { get; set; }
        public float damage { get; set; }
        public bool isDead { get; set; }

        private void Start()
        {
            health = _enemyInfoDataSO.health;
            speed = _enemyInfoDataSO.speed;
        }

        public void TakingDamage(float amount)
        {
            health -= amount;
            if (health <= 0 && !isDead)
                OnEnemyDie();
        }

        private void OnEnemyDie()
        {
            isDead = true;
            EnemyWaveManager.Instance.OnEnemyDie();
            AfterDie();
            CurrencyDataManager.Instance.AddReward(_enemyInfoDataSO.rewardOnDeath);
        }

        public void AfterDie()
        {
            EnemyMovementHandler.Instance.RemoveEnemy(this);
            EnemyDie();
        }

        public void EnemyDie()
        {
            EnemyWaveManager.Instance.RemoveEnemy(this);
            EnemyBlastEffect enemyBlastEffect = PoolManager.Instance.GetItem<EnemyBlastEffect>(PoolItemConstants.ENEMY_BLAST_EFFECT);
            enemyBlastEffect.transform.localPosition = transform.localPosition;
            enemyBlastEffect.PlayEffect(Color.red);
            Destroy(gameObject);
        }

        public void SetHealthMultiplier(float healthMultiplier)
        {
            float healtToAdd = health * healthMultiplier / 100f;
            health += healtToAdd;
        }
    }
}