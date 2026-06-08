using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public int maxShoot = 5;
    public float timeToReload = 1f;

    [Header("Gun UI")]
    public List<UIGunUpdater> uIGunUpdaters;

    protected int _currentShoots;
    protected bool _isReloading;

    private void Start()
    {
        uIGunUpdaters = UIUpdaterManager.Instance.uIGunUpdaters.ToList();
    }

    protected override IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (_isReloading)
            {
                yield return null;
                continue;
            }

            Shoot();

            yield return new WaitForSeconds(timeBeetweenShoot);
        }
    }

    public override void Shoot()
    {
        if (_isReloading)
            return;

        if (!CanShoot())
            return;

        base.Shoot();

        _currentShoots++;

        UpdateUI();

        if (_currentShoots >= maxShoot)
        {
            StartRecharge();
        }
    }

    protected void StartRecharge()
    {
        if (_isReloading)
            return;

        _isReloading = true;

        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;

        while (time < timeToReload)
        {
            time += Time.deltaTime;

            uIGunUpdaters.ForEach(i =>
                i.UpdateValue(time / timeToReload));

            yield return null;
        }

        _currentShoots = 0;
        _isReloading = false;

        UpdateUI();
    }

    protected void UpdateUI()
    {
        uIGunUpdaters.ForEach(i =>
            i.UpdateValue(maxShoot, _currentShoots));
    }

    public void RefreshUI()
    {
        if (uIGunUpdaters == null || uIGunUpdaters.Count == 0)
        {
            Debug.Log("Lista vazia!");
            return;
        }

        UpdateUI();
    }
}