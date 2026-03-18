using TD.Pool;
using UnityEngine;

public class EnemyBlastEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayEffect(Color color)
    {
        var main = _particleSystem.main;
        main.startColor = color;
        _particleSystem.Play();
        Invoke(nameof(ReturnToPool), _particleSystem.main.duration);
    }

    private void ReturnToPool()
    {
        PoolManager.Instance.ReturnItem(PoolItemConstants.ENEMY_BLAST_EFFECT, this);
    }
}
