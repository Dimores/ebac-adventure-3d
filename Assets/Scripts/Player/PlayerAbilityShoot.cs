using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Gun Config")]
    [SerializeField] private GunBase gun1;
    [SerializeField] private GunBase gun2;
    [SerializeField] private Transform gunPosition;

    private GunBase[] guns;
    private GunBase currentGun;
    private int currentGunIndex;

    protected override void Init()
    {
        base.Init();

        guns = new GunBase[2];

        guns[0] = Instantiate(gun1, gunPosition);
        guns[1] = Instantiate(gun2, gunPosition);

        guns[0].transform.localPosition = Vector3.zero;
        guns[0].transform.localRotation = Quaternion.identity;

        guns[1].transform.localPosition = Vector3.zero;
        guns[1].transform.localRotation = Quaternion.identity;

        currentGun = guns[0];
        currentGunIndex = 0;

        if (currentGun is GunShootLimit gunLimit)
        {
            gunLimit.RefreshUI();
        }


    }

    protected override void RegisterListeners()
    {
        inputs.Gameplay.Shoot.performed += OnShootPerformed;
        inputs.Gameplay.Shoot.canceled += OnShootCanceled;

        inputs.Gameplay.Weapon1.performed += OnWeapon1Performed;
        inputs.Gameplay.Weapon2.performed += OnWeapon2Performed;

        player.healthBase.OnKill += OnPlayerKill;
        player.healthBase.OnRevive += OnPlayerRevive;
    }

    protected override void RemoveListeners()
    {
        inputs.Gameplay.Shoot.performed -= OnShootPerformed;
        inputs.Gameplay.Shoot.canceled -= OnShootCanceled;

        inputs.Gameplay.Weapon1.performed -= OnWeapon1Performed;
        inputs.Gameplay.Weapon2.performed -= OnWeapon2Performed;

        player.healthBase.OnKill -= OnPlayerKill;
        player.healthBase.OnRevive -= OnPlayerRevive;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        currentGun?.StartShoot();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        currentGun?.StopShoot();
    }

    private void OnWeapon1Performed(InputAction.CallbackContext context)
    {
        EquipGun(0);
    }

    private void OnWeapon2Performed(InputAction.CallbackContext context)
    {
        EquipGun(1);
    }

    private void EquipGun(int index)
    {
        if (currentGunIndex == index)
            return;

        currentGun?.StopShoot();

        currentGunIndex = index;
        currentGun = guns[index];

        if (currentGun is GunShootLimit gunLimit)
        {
            gunLimit.RefreshUI();
        }
    }

    private void OnPlayerKill(HealthBase health)
    {
        currentGun?.StopShoot();

        inputs.Disable();
    }

    private void OnPlayerRevive(HealthBase health)
    {
        inputs.Enable();
    }
}