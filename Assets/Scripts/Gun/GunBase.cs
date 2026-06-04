using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBeetweenShoot = .3f;

    private Coroutine _currentCoroutine;

    public KeyCode keyCode = KeyCode.Z;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyCode))
        {
            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBeetweenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }
}
