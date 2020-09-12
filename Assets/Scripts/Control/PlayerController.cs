using System;
using UnityEngine;
using WarRoad.Combat;
using WarRoad.Core;
using WarRoad.Movement;

namespace WarRoad.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health _health;

        void Start()
        {
            _health = GetComponent<Health>();
        }

        void Update()
        {
            if (_health.IsDead)
            {
                return;
            }
            if (InteractWithCombat()) // attack
            {
                return;
            }
            if (InteractWithMovement()) // move
            {
                return;
            }
            Debug.Log("nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                AttackableTarget target = hit.transform.GetComponent<AttackableTarget>();

                if (target == null)
                {
                    continue;
                }

                if (GetComponent<Attacker>().CanAttackTarget(target.gameObject) == true) // ignore dead targets
                {
                    if (Input.GetMouseButton(0))
                    {
                        GetComponent<Attacker>().Attack(target.gameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitInfo; // where the ray hit a colider
            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<CharacterMovementHandler>().StartMoveAction(hitInfo.point);
                }
                return true;
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}