using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class ActionLifePack : ActionBase
    {
        public void Start()
        {
            intData = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        }

        public override void Execute()
        {
            base.Execute();
            
            if(intData.value > 0)
            {
                ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);

                Player.Instance.healthBase.ResetLife();
            }
        }
    }
}
