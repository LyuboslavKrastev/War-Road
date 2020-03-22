﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarRoad.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;

        private bool _isDead = false;

        public bool IsDead { get { return _isDead; } }
        void Start()
        {

        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0); // so that it does not go below 0

            if (_health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead == false)
            {
                _isDead = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }
}
