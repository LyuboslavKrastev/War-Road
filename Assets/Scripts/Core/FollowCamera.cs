using UnityEngine;

namespace WarRoad.Core
{
    public class FollowCamera : MonoBehaviour
    {
        private Camera camera;

        [SerializeField] private const int MAX_CAMERA_FOV = 90;
        [SerializeField] private const int MIN_CAMERA_FOV = 60;

        [SerializeField] private Transform _target;


        [SerializeField] private float speed = 8f;

        private void Start()
        {
            camera = this.GetComponentInChildren<Camera>();
        }

        void LateUpdate()
        {
            transform.position = _target.position;
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                RotateCamera();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
            {
                if (camera.fieldOfView < MAX_CAMERA_FOV)
                {
                    camera.fieldOfView+=2;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (camera.fieldOfView > MIN_CAMERA_FOV)
                {
                    camera.fieldOfView-=2;
                }
            }
        }

        private void RotateCamera()
        {
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0));
            var X = transform.rotation.eulerAngles.x;
            var Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }
}