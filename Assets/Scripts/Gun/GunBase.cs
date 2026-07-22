using System; // <- Importante para usar o Action
using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public event Action OnShoot; // Evento do tiro

    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBeetweenShoot = .3f;
    public float speed = 50f;

    protected Coroutine _currentCoroutine;
    private float _nextShootTime;
    private Player _player;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBeetweenShoot);
        }
    }

    protected bool CanShoot()
    {
        if (Time.time < _nextShootTime)
            return false;

        _nextShootTime = Time.time + timeBeetweenShoot;
        return true;
    }

    public virtual void Shoot()
    {
        if (!CanShoot())
            return;

        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        OnShoot?.Invoke();
    }

    public void StartShoot()
    {
        if (_currentCoroutine != null)
            return;

        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}