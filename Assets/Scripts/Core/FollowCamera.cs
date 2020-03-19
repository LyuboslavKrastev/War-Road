using UnityEngine;

namespace WarRoad.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}