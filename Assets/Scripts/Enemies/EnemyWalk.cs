using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        public GameObject[] wayPoints;
        public float minDistance = 1f;
        public float speed = 10f;

        private int _currentWayPointIndex = 0;
        private bool _canMove = true;

        private void Start()
        {
            _canMove = true;
            _currentWayPointIndex = 0;
        }

        protected override void OnKill()
        {
            base.OnKill();

            _canMove = false;
        }

        public override void Update()
        {
            base.Update();

            if (!_canMove) return;

            if (Vector3.Distance(transform.position, wayPoints[_currentWayPointIndex].transform.position) < minDistance)
            {
                _currentWayPointIndex++;
                if (_currentWayPointIndex >= wayPoints.Length) _currentWayPointIndex = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, 
                wayPoints[_currentWayPointIndex].transform.position, Time.deltaTime * speed);

            transform.LookAt(wayPoints[_currentWayPointIndex].transform);
        }
    }
}
