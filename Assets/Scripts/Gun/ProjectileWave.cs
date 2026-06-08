using UnityEngine;

public class ProjectileWave : ProjectileBase
{
    public float waveAmplitude = 2f;
    public float waveFrequency = 5f;

    private Vector3 startPosition;
    private float lifeTime;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        lifeTime += Time.deltaTime;

        transform.position += transform.forward * speed * Time.deltaTime;

        transform.position += transform.right *
                              Mathf.Sin(lifeTime * waveFrequency) *
                              waveAmplitude *
                              Time.deltaTime;
    }
}