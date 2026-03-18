using System.Collections;
using TD.Manager;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color hitColor = new();
    [SerializeField] private float hitDuration;
    [SerializeField] private int flashes;

    private Color _originalColor = Color.white;
    private Coroutine _hitRoutine;

    public void PlayHitEffect(float damage)
    {
        StopHitEffect();
        _hitRoutine = StartCoroutine(HitFlashRoutine());
        TakingDamage(damage);
    }

    private void StopHitEffect()
    {
        if (_hitRoutine != null)
            StopCoroutine(_hitRoutine);
        _hitRoutine = null;
        SetColor(_originalColor);
    }

    private IEnumerator HitFlashRoutine()
    {
        CacheOriginalColor();

        int count = Mathf.Max(1, flashes);
        float duration = Mathf.Max(0.01f, hitDuration);

        for (int i = 0; i < count; i++)
        {
            SetColor(hitColor);
            yield return new WaitForSeconds(duration);
            SetColor(_originalColor);
            yield return new WaitForSeconds(duration);
        }

        _hitRoutine = null;
    }

    private void CacheOriginalColor()
    {
        _originalColor = meshRenderer.material.color;
    }

    private void SetColor(Color c)
    {
        meshRenderer.material.color = c;
    }

    private void TakingDamage(float amount)
    {
        GameManager.Instance.OnEnemyHitPlayerBase();
    }
}
