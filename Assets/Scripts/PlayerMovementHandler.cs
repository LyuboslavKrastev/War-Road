using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Ray _lastRay;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }


    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo; // where the ray hit a colider
        bool hasHit = Physics.Raycast(ray, out hitInfo);

        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }
    }
}
