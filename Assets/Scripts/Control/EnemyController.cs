using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WarRoad.Combat;
using WarRoad.Core;

namespace WarRoad.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;

        private GameObject _player;

        private Attacker _attacker;

        private Health _health;

        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _attacker = GetComponent<Attacker>();
            _health = GetComponent<Health>();
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
                _attacker.Cancel();
            }
        }
    }
}
