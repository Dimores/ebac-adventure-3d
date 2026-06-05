using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => StopShoot();
    }
    private void StartShoot()
    {
        gunBase.StartShoot();
    }

    private void StopShoot()
    {
        gunBase.StopShoot();
    }

}

