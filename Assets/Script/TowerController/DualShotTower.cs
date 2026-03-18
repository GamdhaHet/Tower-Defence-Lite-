using System.Collections;
using TD.Pool;
using UnityEngine;

namespace TD.TowerHandler
{
    public class DualShotTower : BaseTower
    {
        public int numberOfBullets = 2;
        [SerializeField] private float bulletSpread = 0.25f;
        [SerializeField] private float delayBetweenBullets = 0.1f;

        private Coroutine _shootRoutine;

        protected override void OnShoot()
        {
            if (numberOfBullets <= 0)
                return;

            if (_shootRoutine != null)
                StopCoroutine(_shootRoutine);

            _shootRoutine = StartCoroutine(ShootBurst());
        }

        private IEnumerator ShootBurst()
        {
            float mid = (numberOfBullets - 1) * 0.5f;
            float wait = Mathf.Max(0f, delayBetweenBullets);

            for (int i = 0; i < numberOfBullets; i++)
            {
                float offset = (i - mid) * bulletSpread;
                FireBullet(offset);

                if (i < numberOfBullets - 1 && wait > 0f)
                    yield return new WaitForSeconds(wait);
            }

            _shootRoutine = null;
        }

        private void FireBullet(float offset)
        {
            Bullet bullet = PoolManager.Instance.GetItem<Bullet>(PoolItemConstants.BULLET);
            bullet.transform.SetParent(null);
            bullet.transform.position = firePoint.position + (firePoint.right * offset);
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetTarget(target, GetDamage());
        }
    }
}