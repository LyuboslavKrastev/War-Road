using UnityEngine;
using WarRoad.Core;
using WarRoad.Interfaces;
using WarRoad.Movement;

namespace WarRoad.Combat
{
    public class Attacker : MonoBehaviour, IAction
    {
        [SerializeField] float _attackRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _damage = 5f;

        private float _timeSinceLastAttack = 0;

        private Transform _target;
        private CharacterMovementHandler _characterMovementHandler;
        void Start()
        {
            _characterMovementHandler = GetComponent<CharacterMovementHandler>();
        }
        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null)
            {
                return;
            }

            if (IsInRange() == true)
            {
                // attack
                _characterMovementHandler
                    .Cancel();
                TriggerAttackBehavior();
            }
            else
            {
                // move to target
                _characterMovementHandler
                   .MoveTo(_target.position);
            }
        }

        private void TriggerAttackBehavior()
        {
            AttackIfCooledDown();

        }

        private void AttackIfCooledDown()
        {
            if (_timeSinceLastAttack >= _timeBetweenAttacks)
            {
                // The animation is calling the Hit() method
                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }
        void Hit()
        {
            _target.GetComponent<Health>().TakeDamage(_damage);
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
