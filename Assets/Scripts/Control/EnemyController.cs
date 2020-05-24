using System;
using UnityEngine;
using WarRoad.Combat;
using WarRoad.Core;
using WarRoad.Movement;

namespace WarRoad.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;
        [SerializeField] private float _lingerTime = 3f;
        [SerializeField] private PatrolPath _patrolPath;
        [SerializeField] private float _waypointTolerance = 1f;
       
        [SerializeField] private float _waypointLingerTime = 2f;

        private CharacterMovementHandler _mover;

        private GameObject _player;

        private Attacker _attacker;

        private Health _health;

        private float _timeSinceLastPlayerDetection = Mathf.Infinity;
        private float _timeSinceReachedWaypoint = Mathf.Infinity;


        private Vector3 _initialPosition; // guard location

        private int _currentWaypointIndex = 0;

        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _attacker = GetComponent<Attacker>();
            _health = GetComponent<Health>();
            _initialPosition = transform.position;
            _mover = GetComponent<CharacterMovementHandler>();
        }
        void Update()
        {
            if (_health.IsDead)
            {
                return;
            }

            bool isInAttackRange = Vector3.Distance(_player.transform.position, transform.position) <= _chaseDistance;
            if (isInAttackRange)
            {
                if (_attacker.CanAttackTarget(_player))
                {
                    _timeSinceLastPlayerDetection = 0;
                    AttackBehaviour();
                }
            }
            else if (_timeSinceLastPlayerDetection < _lingerTime)
            {
                LingerBehavior();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastPlayerDetection += Time.deltaTime;
            _timeSinceReachedWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _initialPosition;

            if (_patrolPath != null)
            {
                if (AtWayPoint())
                {
                    _timeSinceReachedWaypoint = 0;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if (_timeSinceReachedWaypoint > _waypointLingerTime)
            {
                _mover.StartMoveAction(nextPosition);
            }
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            if (distanceToWaypoint < _waypointTolerance)
            {
                return true;
            }
            return false;
        }

        private Vector3 GetCurrentWayPoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void CycleWayPoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private void LingerBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _attacker.Attack(_player);
        }

        // Show the chase distance
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
