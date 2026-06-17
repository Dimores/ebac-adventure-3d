using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public int maxShoot = 5;
    public float timeToReload = 1f;

    [Header("Gun UI")]
    public List<UIFillUpdater> uIGunUpdaters;

    protected int _currentShoots;
    protected bool _isReloading;

    private float _reloadFinishTime;

    private void Start()
    {
        uIGunUpdaters = UIUpdaterManager.Instance.uIGunUpdaters.ToList();
    }

    private void Update()
    {
        if (!_isReloading)
            return;

        float remaining = _reloadFinishTime - Time.time;

        if (remaining <= 0)
        {
            _currentShoots = 0;
            _isReloading = false;

            UpdateUI();
            return;
        }

        float progress = 1f - (remaining / timeToReload);

        uIGunUpdaters.ForEach(i =>
            i.UpdateValue(progress));
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
        _reloadFinishTime = Time.time + timeToReload;
    }

    protected void UpdateUI()
    {
        uIGunUpdaters.ForEach(i =>
            i.UpdateValue(maxShoot, _currentShoots));
    }

    public void RefreshUI()
    {
        UpdateUI();
    }
}