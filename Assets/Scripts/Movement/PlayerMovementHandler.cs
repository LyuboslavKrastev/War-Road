using System;
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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
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

    private void UpdateAnimator()
    {
        // get velocity from the navmesh agent
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // convert it to local velocity, so that it's meaningful for the animator
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", speed);


    }

}
