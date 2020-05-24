using UnityEngine;
using WarRoad.Combat;
using WarRoad.Core;
using WarRoad.Movement;

namespace WarRoad.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;

        private CharacterMovementHandler _mover;

        private GameObject _player;

        private Attacker _attacker;

        private Health _health;

        private Vector3 _initialPosition; // guard location

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
                    _attacker.Attack(_player);
                }             
            }
            else
            {
                _mover.StartMoveAction(_initialPosition);
            }
        }

        // Show the chase distance
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
