using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Ebac.Managers;
using Items;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Animation Config")]
    public float rotationSpeed = 1f;
    public float floatHeight = 0.2f;
    public float floatDuration = 1f;

    [Header("VFX Collider")]
    public Transform vfxCollider;

    private Tween _rotationTween;
    private Tween _floatTween;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(_rb == null)
            AnimateCoin();
    }

    private void AnimateCoin()
    {
        _floatTween = transform.DOMoveY(transform.position.y + floatHeight, floatDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void KillAnimations()
    {
        _rotationTween?.Kill();
        _floatTween?.Kill();
    }

    protected override void OnCollect()
    {
        base.OnCollect();

        if(VFXManager.Instance != null)
        {
            VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.COIN, this.transform.position,
                null, vfxCollider);
        }
    }

    private void OnDestroy()
    {
        _floatTween?.Kill();
    }
}