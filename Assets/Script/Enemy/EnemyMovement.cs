using TD.EnemyMovement;
using TD.Manager;
using UnityEngine;

namespace TD.EnemyHandler
{
    public class EnemyMovement : MonoBehaviour //TODO: Remove this class
    {
        [SerializeField] private Enemy _enemy;
        private Checkpoint target;
        private int wavepointIndex = 0;

        private void Start()
        {
            target = CheckpointsManager.Instance.defaultCheckpoint;
        }

        private void Update()
        {
            Vector3 dir = target.CheckpointTransform.position - transform.position;
            transform.Translate(dir.normalized * _enemy.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.CheckpointTransform.position) <= 0.4f)
                GetNextWaypoint();
        }

        private void GetNextWaypoint()
        {
            if (wavepointIndex >= CheckpointsManager.Instance.Checkpoints.Count - 1)
            {
                OnReachToPlayerBase();
                return;
            }

            wavepointIndex++;
            target = CheckpointsManager.Instance.Checkpoints[wavepointIndex];
        }

        private void OnReachToPlayerBase()
        {
            PlayerInfoManager.Instance.Lives--;
            GameManager.Instance.playerBase.PlayHitEffect(_enemy.damage);
            EnemyWaveManager.Instance.OnEnemyDie();
            Destroy(gameObject);
        }
    }
}