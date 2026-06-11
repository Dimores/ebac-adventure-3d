using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemySuicide : EnemyBase
    {
        [Header("Suicide Chase Settings")]
        public float speed = 5f;
        public float chaseDistance = 10f;
        public float explodeDistance = 2.0f; 
        public float explosionDelay = 1.5f;
        public float explosionDamage = 3f;

        [Header("Custom Flash Settings")]
        public MeshRenderer meshRenderer;
        public Color customFlashColor = Color.red;

        private bool _isChasing = false;
        private bool _isExploding = false;
        private Color _originalColor;
        private Rigidbody _rb; 
        private bool _canMove = true;

        protected override void Start()
        {
            base.Start();

            _rb = GetComponent<Rigidbody>(); 

            if (meshRenderer != null)
                _originalColor = meshRenderer.material.color;

            _canMove = true;
        }

        protected override void OnKill()
        {
            base.OnKill();
            _isChasing = false;
            _isExploding = false;
            _canMove = false;
            StopAllCoroutines();
            
            if (meshRenderer != null)
            {
                meshRenderer.material.DOKill();
            }
        }

        public override void Update()
        {
            base.Update();

            if (!_canMove) return;

            if (_player == null || _isExploding) return;

            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (!_isChasing && distanceToPlayer <= chaseDistance)
            {
                _isChasing = true;
            }

            if (_isChasing)
            {
                Vector3 targetDirection = _player.transform.position - transform.position;
                targetDirection.y = 0; 
                if (targetDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(targetDirection);
                }

                if (distanceToPlayer > explodeDistance)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        _player.transform.position,
                        speed * Time.deltaTime
                    );
                }
                else
                {
                    StartCoroutine(ExplosionSequence());
                }
            }
        }

        private IEnumerator ExplosionSequence()
        {
            _isExploding = true;
            _isChasing = false;

            if (_rb != null)
            {
                _rb.velocity = Vector3.zero; 
                _rb.isKinematic = true; 
            }

            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }

            transform.DOScale(transform.localScale * 1.3f, explosionDelay).SetEase(Ease.InQuad);

            if (meshRenderer != null)
            {
                StartCoroutine(FlashRoutine());
            }

            yield return new WaitForSeconds(explosionDelay);

            if (_player != null)
            {
                float finalDistance = Vector3.Distance(transform.position, _player.transform.position);
                if (finalDistance <= explodeDistance + 1.0f) 
                {
                    _player.Damage(explosionDamage);
                }
            }

            if (damageVFX != null)
            {
                damageVFX.transform.SetParent(null);
                damageVFX.Play();
                Destroy(damageVFX.gameObject, 2f);
            }

            Destroy(gameObject);
        }

        private IEnumerator FlashRoutine()
        {
            float elapsed = 0f;
            float currentFlashInterval = 0.3f; 
            float minInterval = 0.05f; 

            while (elapsed < explosionDelay && _isExploding)
            {
                meshRenderer.material.DOColor(customFlashColor, currentFlashInterval * 0.4f);
                yield return new WaitForSeconds(currentFlashInterval * 0.5f);

                meshRenderer.material.DOColor(_originalColor, currentFlashInterval * 0.4f);
                yield return new WaitForSeconds(currentFlashInterval * 0.5f);

                elapsed += currentFlashInterval;

                currentFlashInterval = Mathf.Max(minInterval, currentFlashInterval * 0.7f);
            }
        }
    }
}