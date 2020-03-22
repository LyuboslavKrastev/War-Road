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

        private Health _target;
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
            if (_target.IsDead)
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
                   .MoveTo(_target.transform.position);
            }
        }

        private void TriggerAttackBehavior()
        {
            transform.LookAt(_target.transform);
            bool canAttack = _timeSinceLastAttack >= _timeBetweenAttacks;

            if (canAttack)
            {
                StartAttackAnimation();
                _timeSinceLastAttack = 0;
            }
        }

        public bool CanAttackTarget(AttackableTarget target)
        {
            if (target == null)
            {
                return false;
            }
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth == null)
            {
                return false;
            }
            return !targetHealth.IsDead;
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < _attackRange;
        }

        public void Attack(AttackableTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttackAnimation();
            _target = null;
        }
        private void StartAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }
        private void StopAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Called by the attack animation
        void Hit()
        {
            if (_target != null)
            {
                _target.TakeDamage(_damage);
            }
        }
    }
}
