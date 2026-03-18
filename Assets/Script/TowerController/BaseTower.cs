using System.Collections;
using TD.EnemyHandler;
using TD.GamePlay;
using TD.Manager;
using TD.Pool;
using UnityEngine;

namespace TD.TowerHandler
{
    public abstract class BaseTower : MonoBehaviour
    {
        [SerializeField] protected TowerInfoDataSO _towerInfoDataSO;
        protected Transform target;
        private float fireCountdown = 0f;
        private EnemyWaveManager _enemyWaveManager;
        private Coroutine _updateTargetRoutine;

        public TowerInfoDataSO towerInfoDataSO { get { return _towerInfoDataSO; } }
        public Transform partToRotate;
        public float turnSpeed = 10f;
        public Transform firePoint;


        private void Start()
        {
            _enemyWaveManager = EnemyWaveManager.Instance;
            StopCoroutineUpdateTarget();
            _updateTargetRoutine = StartCoroutine(StartUpdateTargetRoutine());
        }

        private void StopCoroutineUpdateTarget()
        {
            if (_updateTargetRoutine != null)
                StopCoroutine(_updateTargetRoutine);
        }

        private IEnumerator StartUpdateTargetRoutine()
        {
            while (true)
            {
                UpdateTarget();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void UpdateTarget()
        {
            Enemy[] enemies = _enemyWaveManager.GetEnemies();
            float shortestDistance = Mathf.Infinity;
            Enemy nearestEnemy = null;

            foreach (Enemy enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= towerInfoDataSO.range)
                target = nearestEnemy.transform;
            else
                target = null;

        }

        private void Update()
        {
            if (target == null)
                return;

            LockOnTarget();

            if (fireCountdown <= 0f)
            {
                OnShoot();
                fireCountdown = 1f / GetFireRate();
            }

            fireCountdown -= Time.deltaTime;
        }

        protected virtual void OnShoot()
        {
            Bullet bullet = PoolManager.Instance.GetItem<Bullet>(PoolItemConstants.BULLET);
            bullet.transform.SetParent(firePoint);
            bullet.transform.localPosition = Vector3.zero;
            bullet.SetTarget(target, GetDamage());
        }

        protected virtual float GetFireRate()
        {
            return _towerInfoDataSO.fireRate;
        }

        protected virtual float GetDamage()
        {
            return _towerInfoDataSO.damage;
        }

        private void LockOnTarget()
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
}