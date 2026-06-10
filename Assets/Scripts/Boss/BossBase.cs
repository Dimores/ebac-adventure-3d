using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Movement")]
        public float speed = 5f;
        public List<Transform> wayPoints;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        private StateMachine<BossAction> stateMachine;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.IDLE, new BossStateIdle());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());

            SwitchState(BossAction.INIT);
        }

        #region MOVEMENT
        public void GoToRandomPoint()
        {
            StartCoroutine(MoveToPoint(wayPoints[Random.Range(0, wayPoints.Count)]));
        }

        IEnumerator MoveToPoint(Transform t)
        {
            while (Vector3.Distance(transform.position, t.position) > .1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion

        #region ANIMATION
        public void PlayStartAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
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
