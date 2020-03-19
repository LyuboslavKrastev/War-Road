using System;
using UnityEngine;
using WarRoad.Combat;
using WarRoad.Movement;

namespace WarRoad.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (InteractWithCombat())
            {
                return;
            }
            if (InteractWithMovement())
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

                if (target != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Attacker>().Attack(target);
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