using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Start()
        {
            base.Start();

            _player.healthBase.OnKill += HandlePlayerKill;
            _player.healthBase.OnRevive += HandlePlayerRevive;
        }

        protected override void OnKill()
        {
            base.OnKill();
            gunBase.StopShoot();

            _player.healthBase.OnKill -= HandlePlayerKill;
            _player.healthBase.OnRevive -= HandlePlayerRevive;
        }

        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
        }

        private void HandlePlayerKill(HealthBase h)
        {
            gunBase.StopShoot();
        }

        private void HandlePlayerRevive(HealthBase h)
        {
            StartCoroutine(StartShootDelayed(2f));
        }
        private IEnumerator StartShootDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            gunBase.StartShoot();
        }
    }
}
