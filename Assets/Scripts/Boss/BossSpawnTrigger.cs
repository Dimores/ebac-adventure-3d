using Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BossBase bossBase;
    [SerializeField] private float delayToStartAttacking = 2f;
    [SerializeField] private string playerTag = "Player";

    [Header("Camera")]
    [SerializeField] private GameObject bossCamera;

    [Header("Gizmo")]
    [SerializeField] private Color gizmoColor = Color.yellow;

    [Header("Collider")]
    [SerializeField] private BoxCollider triggerCollider;

    private bool _hasTriggerOnce = false;

    private void Awake()
    {
        _hasTriggerOnce = false;

        TurnCamera(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggerOnce) return;

        if (other.transform.tag == playerTag)
        {
            _hasTriggerOnce = true;
            bossBase.Init();

            StartCoroutine(StartAttackStateWithDelay());

            TurnCamera(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, transform.localScale * triggerCollider.size.x);
    }

    private IEnumerator StartAttackStateWithDelay()
    {
        yield return new WaitForSeconds(delayToStartAttacking);
        bossBase.SwitchState(BossAction.ATTACK);
    }

    #region CAMERA
    private void TurnCamera(bool willEnable)
    {
        bossCamera.SetActive(willEnable);
    }
    #endregion
}