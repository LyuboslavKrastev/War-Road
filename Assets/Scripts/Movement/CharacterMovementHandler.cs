using UnityEngine;
using UnityEngine.AI;
using WarRoad.Core;
using WarRoad.Interfaces;

namespace WarRoad.Movement
{
    public class CharacterMovementHandler : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;
        private Health _health;
        [SerializeField] private float _maxSpeed = 10f;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
        }
        void Update()
        {
            if (_health.IsDead)
            {
                _navMeshAgent.enabled = false;
            }
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point, float speedFraction)
        {
            this.GetComponent<ActionScheduler>().StartAction(this);
            this.MoveTo(point, speedFraction);
        }

        public void MoveTo(Vector3 point, float speedFraction)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
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