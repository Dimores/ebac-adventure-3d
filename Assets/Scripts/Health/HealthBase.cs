using Animation;
using Cloth;
using DG.Tweening;
using Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10;
    public bool destroyOnKill = false;

    [SerializeField] private float _currentLife;
    public float damageMultiply = 1f;

    [Header("UI")]
    [SerializeField] private List<UIFillUpdater> uiFillUpdaters;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public Action<HealthBase> OnRevive;

    public float CurrentHealth
    {
        get { return _currentLife; }
    }

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

    public void Heal(float amount)
    {
        _currentLife += amount;
        if (_currentLife > startLife) _currentLife = startLife;
        UpdateUI();
    }

    public void SetHealth(float health)
    {
        _currentLife = Mathf.Clamp(
            health,
            0f,
            startLife
        );

        UpdateUI();
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiply;

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

    public void ChangeDamageMultiply(float damageMultiply, float duration)
    {
        StartCoroutine(SetDamageMultiplyCoroutine(damageMultiply, duration));
    }

    IEnumerator SetDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1f;
    }
}
