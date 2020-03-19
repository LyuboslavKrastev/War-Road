using UnityEngine;
using UnityEngine.AI;
using WarRoad.Core;
using WarRoad.Interfaces;

namespace WarRoad.Movement
{
    public class CharacterMovementHandler : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(point);
        }

        public void MoveTo(Vector3 point)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = point;
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
        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
    }

}