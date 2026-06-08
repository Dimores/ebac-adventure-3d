using Animation;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [Header("Life")]
        public float startLife = 10f;
        [SerializeField] private float _currentLife;
        public float deathDelay = 3f;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool playStartAnimation = true;

        [Header("Damage Animation")]
        public float damageAnimationDuration = .1f;
        public Ease damageAnimationEase = Ease.Linear;
        public Color damageColor = Color.white;
        public Color damageEmissionColor = Color.white;

        private readonly List<SpriteRenderer> _spriteRenderers = new();
        private readonly List<Material> _materials = new();

        private readonly Dictionary<SpriteRenderer, Color> _spriteOriginalColors = new();
        private readonly Dictionary<Material, Color> _materialOriginalColors = new();
        private readonly Dictionary<Material, Color> _materialOriginalEmissionColors = new();

        private readonly Dictionary<Object, Tween> _activeColorTweens = new();
        private readonly Dictionary<Material, Tween> _activeEmissionTweens = new();

        private void Awake()
        {
            CacheRenderers();
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();

            if (playStartAnimation)
                BornAnimation();
        }

        private void CacheRenderers()
        {
            _spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));

            foreach (var sprite in _spriteRenderers)
            {
                _spriteOriginalColors[sprite] = sprite.color;
            }

            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.materials;

                foreach (Material material in materials)
                {
                    _materials.Add(material);

                    if (material.HasProperty("_Color"))
                    {
                        _materialOriginalColors[material] = material.color;
                    }

                    if (material.HasProperty("_EmissionColor"))
                    {
                        _materialOriginalEmissionColors[material] =
                            material.GetColor("_EmissionColor");
                    }
                }
            }
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        public void OnDamage(float f)
        {
            _currentLife -= f;

            DamageAnimation();

            if (_currentLife <= 0)
                Kill();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
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

        private void DamageAnimation()
        {
            foreach (SpriteRenderer sprite in _spriteRenderers)
            {
                if (_activeColorTweens.TryGetValue(sprite, out Tween tween))
                {
                    tween.Kill();
                }

                sprite.color = damageColor;

                _activeColorTweens[sprite] = sprite
                    .DOColor(_spriteOriginalColors[sprite], damageAnimationDuration)
                    .SetEase(damageAnimationEase);
            }

            foreach (Material material in _materials)
            {
                if (material.HasProperty("_Color"))
                {
                    if (_activeColorTweens.TryGetValue(material, out Tween tween))
                    {
                        tween.Kill();
                    }

                    material.color = damageColor;

                    _activeColorTweens[material] = material
                        .DOColor(
                            _materialOriginalColors[material],
                            damageAnimationDuration
                        )
                        .SetEase(damageAnimationEase);
                }

                if (material.HasProperty("_EmissionColor"))
                {
                    if (_activeEmissionTweens.TryGetValue(material, out Tween tween))
                    {
                        tween.Kill();
                    }

                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", damageEmissionColor);

                    _activeEmissionTweens[material] = DOTween.To(
                            () => material.GetColor("_EmissionColor"),
                            value => material.SetColor("_EmissionColor", value),
                            _materialOriginalEmissionColors[material],
                            damageAnimationDuration
                        )
                        .SetEase(damageAnimationEase);
                }
            }
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

        #endregion
    }
}