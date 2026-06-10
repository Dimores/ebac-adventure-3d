using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;

    [Header("Hit")]
    [SerializeField] private LayerMask hitMask;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            0.3f,
            hitMask
        );

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(damageAmount);

                Destroy(gameObject);
                return;
            }
        }
    }
}