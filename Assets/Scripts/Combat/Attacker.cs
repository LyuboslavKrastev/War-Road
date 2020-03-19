using UnityEngine;
using WarRoad.Core;
using WarRoad.Interfaces;
using WarRoad.Movement;

namespace WarRoad.Combat
{
    public class Attacker : MonoBehaviour, IAction
    {
        [SerializeField] float _attackRange = 2f;

        private Transform _target;
        private CharacterMovementHandler _characterMovementHandler;
        void Start()
        {
            _characterMovementHandler = GetComponent<CharacterMovementHandler>();
        }
        void Update()
        {
            if (_target == null)
            {
                return;
            }

            if (IsInRange() == true)
            {
                Debug.Log("attacking");
                _characterMovementHandler
                    .Cancel();
            }
            else
            {
                Debug.Log("moving to target");
                _characterMovementHandler
                   .MoveTo(_target.position);
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.position, transform.position) < _attackRange;
        }

        public void Attack(AttackableTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = target.transform;
        }

        public void Cancel()
        {
            _target = null;
        }
    }
}
