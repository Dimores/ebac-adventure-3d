using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class DestructableItemBase : MonoBehaviour
{
    [Header("References")]
    public HealthBase healthBase;

    [Header("Punch Scale")]
    public Vector3 punchForce = new Vector3(0.15f, 0.15f, 0f);
    [MinMaxSlider(0.05f, 0.5f)]
    public Vector2 punchDuration = new Vector2(0.10f, 0.15f);
    public Vector2Int punchVibrato = new Vector2Int(8, 12);
    [MinMaxSlider(0f, 1f)]
    public Vector2 punchElasticity = new Vector2(0.7f, 1f);

    [Header("Rotation Shake")]
    public Vector3 rotationStrength = new Vector3(0f, 0f, 4f);
    [MinMaxSlider(0.05f, 0.5f)]
    public Vector2 rotationDuration = new Vector2(0.10f, 0.15f);
    public Vector2Int rotationVibrato = new Vector2Int(10, 16);
    public float rotationRandomness = 90f;

    private void OnValidate()
    {
        if (healthBase == null)
            healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        if (healthBase != null)
            healthBase.OnDamage += OnDamage;
    }

    private void OnDestroy()
    {
        if (healthBase != null)
            healthBase.OnDamage -= OnDamage;
    }

    [Button]
    public void AnimationTeste()
    {
        OnDamage(healthBase);
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOKill();

        Vector3 randomPunch = new Vector3(
            Random.Range(0f, punchForce.x),
            Random.Range(0f, punchForce.y),
            Random.Range(0f, punchForce.z));

        transform.DOPunchScale(
            randomPunch,
            Random.Range(punchDuration.x, punchDuration.y),
            Random.Range(punchVibrato.x, punchVibrato.y + 1),
            Random.Range(punchElasticity.x, punchElasticity.y));

        Vector3 randomRotation = new Vector3(
            Random.Range(-rotationStrength.x, rotationStrength.x),
            Random.Range(-rotationStrength.y, rotationStrength.y),
            Random.Range(-rotationStrength.z, rotationStrength.z));

        transform.DOShakeRotation(
            Random.Range(rotationDuration.x, rotationDuration.y),
            randomRotation,
            Random.Range(rotationVibrato.x, rotationVibrato.y + 1),
            rotationRandomness,
            false);
    }
}