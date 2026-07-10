using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Items;

public class MagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<ItemCollectableCoin>(out var item))
            return;

        if (!other.TryGetComponent<Magnetic>(out _))
        {
            item.KillAnimations();
            other.gameObject.AddComponent<Magnetic>();
        }
    }
}
