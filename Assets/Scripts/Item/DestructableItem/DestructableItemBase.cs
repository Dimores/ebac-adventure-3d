using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using Sound;

public class DestructableItemBase : MonoBehaviour
{
    [Header("References")]
    public HealthBase healthBase;
    public BoxCollider boxCollider;

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

    [Header("Drops")]
    [Min(1)]
    public int dropAmount = 10;

    public GameObject dropPrefab;

    [Min(0f)]
    public float dropSpawnOffset = 0.02f;

    [Tooltip("Metade da largura da moeda (ou do collider dela).")]
    [Min(0f)]
    public float dropRadius = 0.05f;

    private void OnValidate()
    {
        if (healthBase == null)
            healthBase = GetComponent<HealthBase>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();
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
        SFXPool.Instance.Play(SFXType.TYPE_02, new Vector2(0.8f, 1.2f));

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

        Drop();
    }

    private void Drop()
    {
        if (dropPrefab == null || boxCollider == null)
            return;

        Bounds bounds = boxCollider.bounds;

        for (int i = 0; i < dropAmount; i++)
        {
            int side = Random.Range(0, 4);

            Vector3 spawnPosition = Vector3.zero;

            switch (side)
            {
                // Frente
                case 0:
                    spawnPosition = new Vector3(
                        Random.Range(bounds.min.x, bounds.max.x),
                        bounds.max.y + dropSpawnOffset,
                        bounds.max.z + dropSpawnOffset);
                    break;

                // Trás
                case 1:
                    spawnPosition = new Vector3(
                        Random.Range(bounds.min.x, bounds.max.x),
                        bounds.max.y + dropSpawnOffset,
                        bounds.min.z - dropSpawnOffset);
                    break;

                // Direita
                case 2:
                    spawnPosition = new Vector3(
                        bounds.max.x + dropSpawnOffset,
                        bounds.max.y + dropSpawnOffset,
                        Random.Range(bounds.min.z, bounds.max.z));
                    break;

                // Esquerda
                case 3:
                    spawnPosition = new Vector3(
                        bounds.min.x - dropSpawnOffset,
                        bounds.max.y + dropSpawnOffset,
                        Random.Range(bounds.min.z, bounds.max.z));
                    break;
            }

            var item = Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
            item.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
        }
    }
}