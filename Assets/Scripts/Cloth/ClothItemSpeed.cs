using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public override void Collect()
        {
            base.Collect();
            //Player.Instance.SetSpeed(2f, 5f);
        }
    }
}
