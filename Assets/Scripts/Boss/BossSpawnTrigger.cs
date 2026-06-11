using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [SerializeField] private BossBase bossBase;
    [SerializeField] private float delayToStartAttacking = 2f;

    private bool _hasTriggerOnce = false;

    private void Awake()
    {
        _hasTriggerOnce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggerOnce) return;

        if (other.transform.tag == "Player")
        {
            _hasTriggerOnce = true;
            bossBase.Init();

            StartCoroutine(StartAttackStateWithDelay());
        }
    }

    private IEnumerator StartAttackStateWithDelay()
    {
        yield return new WaitForSeconds(delayToStartAttacking);
        bossBase.SwitchState(BossAction.ATTACK);
    }
}