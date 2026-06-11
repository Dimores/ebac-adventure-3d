using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
using System;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        [Header("Health")]
        public HealthBase healthBase;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBeetweenAttacks = .5f;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        private StateMachine<BossAction> stateMachine;

        private void Start()
        {
            healthBase.OnKill += OnBossKill;
        }

        public void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.IDLE, new BossStateIdle());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchState(BossAction.INIT);
        }

        #region HANDLERS
        private void OnBossKill(HealthBase healthBase)
        {
            SwitchState(BossAction.DEATH);
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        public void SwitchToAttackState()
        {
            stateMachine.SwitchState(BossAction.ATTACK, this);
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(Attack(endCallback));
        }

        IEnumerator Attack(Action endCallback = null)
        {
            int attacksDone = 0;

            while(attacksDone < attackAmount)
            {
                attacksDone++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBeetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(MoveToPoint(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], onArrive));
        }

        IEnumerator MoveToPoint(Transform t, Action onArrive = null)
        {
            while (true)
            {
                Vector3 targetPosition = t.position;
                targetPosition.y = transform.position.y;

                if (Vector3.Distance(transform.position, targetPosition) <= .1f)
                {
                    break;
                }

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void PlayStartAnimation()
        {
            transform.localScale = Vector3.zero;

            transform.DOScale(Vector3.one, startAnimationDuration).SetEase(startAnimationEase);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}
