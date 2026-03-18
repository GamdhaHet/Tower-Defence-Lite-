using UnityEngine;
using TD.EnemyHandler;
using TD.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 70f;
    private Transform targetEnemy;
    private float damage;

    public void SetTarget(Transform _target, float _damage)
    {
        targetEnemy = _target;
        this.damage = _damage;
    }

    private void Update()
    {
        if (targetEnemy == null)
        {
            PoolManager.Instance.ReturnItem(PoolItemConstants.BULLET, this);
            return;
        }

        Vector3 dir = targetEnemy.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            DamageToEnemy();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(targetEnemy.transform);

    }

    private void DamageToEnemy()
    {
        Enemy enemy = targetEnemy.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakingDamage(damage);

        PoolManager.Instance.ReturnItem(PoolItemConstants.BULLET, this);
    }
}
