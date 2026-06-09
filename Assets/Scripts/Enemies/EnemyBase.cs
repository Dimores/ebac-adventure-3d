using Animation;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Life")]
        public float startLife = 10f;
        [SerializeField] private float _currentLife;
        public float deathDelay = 3f;
        public Collider enemyCollider;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;
        [SerializeField] private FlashColor flashColor;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool playStartAnimation = true;

        [Header("Damage Animation")]
        public float damageAnimationDuration = .1f;
        public Ease damageAnimationEase = Ease.Linear;
        public Color damageColor = Color.white;
        public Color damageEmissionColor = Color.white;

        [Header("VFX")]
        public ParticleSystem damageVFX;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();

            if (playStartAnimation)
                BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
            enemyCollider.enabled = true;
        }

        public void OnDamage(float f)
        {
            if(flashColor != null) flashColor.Flash();

            if (damageVFX != null) damageVFX.Play();

            _currentLife -= f;

            if (_currentLife <= 0)
                Kill();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            enemyCollider.enabled = false;

            Destroy(gameObject, deathDelay);

            PlayAnimationByType(AnimationType.DEATH);
        }

        #region ANIMATION

        private void BornAnimation()
        {
            transform
                .DOScale(0, startAnimationDuration)
                .SetEase(startAnimationEase)
                .From();
        }

        public void PlayAnimationByType(AnimationType animationType)
        {
            if (_animationBase != null)
            {
                _animationBase.PlayAnimationByType(animationType);
            }
        }

        #endregion

        #region DEBUG

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        #endregion
    }
}