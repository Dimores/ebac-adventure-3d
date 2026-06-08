using UnityEngine;

public class GunShootWave : GunShootLimit
{
    public float angleStep = 15f;
    public int maxOffset = 4;

    private int currentOffset;
    private bool positiveSide = true;

    public override void Shoot()
    {
        if (_isReloading)
            return;

        if (!CanShoot())
            return;

        _currentShoots++;

        UpdateUI();

        if (_currentShoots >= maxShoot)
        {
            StartRecharge();
        }

        float angle = currentOffset * angleStep;

        if (!positiveSide)
            angle *= -1;

        var projectile = Instantiate(prefabProjectile);

        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation =
            positionToShoot.rotation *
            Quaternion.Euler(0, angle, 0);

        projectile.speed = speed;

        positiveSide = !positiveSide;

        if (positiveSide)
        {
            currentOffset++;

            if (currentOffset > maxOffset)
                currentOffset = 0;
        }
    }
}