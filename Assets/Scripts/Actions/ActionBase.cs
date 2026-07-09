using UnityEngine;
using Items;

namespace Actions
{
    public class ActionBase : MonoBehaviour
    {
        public IntData intData;

        public virtual void Execute() { }
    }
}