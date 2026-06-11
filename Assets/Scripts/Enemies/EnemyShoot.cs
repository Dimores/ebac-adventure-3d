using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void OnKill()
        {
            base.OnKill();
            gunBase.StopShoot();
        }

        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
        }
    }
}
