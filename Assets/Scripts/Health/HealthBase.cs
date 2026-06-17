using Animation;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10;
    public bool destroyOnKill = false;

    [SerializeField] private float _currentLife;

    [Header("UI")]
    [SerializeField] private List<UIFillUpdater> uiFillUpdaters;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public Action<HealthBase> OnRevive;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
        OnRevive?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
            Kill();

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiFillUpdaters != null)
        {
            uiFillUpdaters.ForEach(i => i.UpdateValue(startLife, startLife - _currentLife));
        }
    }
}
