using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public int maxShoot = 5;
    public float timeToReload = 1f;

    private int _currentShoots;
    private bool _isReloading = false;

    override protected IEnumerator ShootCoroutine()
    {
        if (_isReloading) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                yield return new WaitForSeconds(timeBeetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _isReloading = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        yield return new WaitForSeconds(timeToReload);
        _currentShoots = 0;
        _isReloading = false;
    }
}
