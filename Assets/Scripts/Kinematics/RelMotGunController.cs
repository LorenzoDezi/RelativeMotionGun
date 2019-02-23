using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinematics
{
    public class RelMotGunController : MonoBehaviour, IAcceleration, IVelocity 
    {
        [SerializeField]
        private float speed = 5f;
        private float maxLength = 24.5f;
        [SerializeField]
        private BallController ballController;
        [SerializeField]
        private float ballUpSpeed = 3f;
        private bool hasShoot = false;


        private void Start()
        {
            if (ballController != null)
                ballController.SetForwardSpeed(transform.forward * speed);
        }

        void Update()
        {
            float newZ = transform.position.z + speed * Time.deltaTime;
            if (UnityEngine.Mathf.Abs(newZ) > maxLength)
            {
                speed = -speed;
                newZ = transform.position.z + speed * Time.deltaTime;
                if (ballController != null)
                    ballController.SetForwardSpeed(transform.forward * speed);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y,
                newZ);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Trigger")) return;
            if (!hasShoot)
                Shoot();
            hasShoot = !hasShoot;
        }

        void Shoot()
        {
            if (ballController != null)
            {
                ballController.transform.parent = null;
                ballController.Launch(new Vector3(0, -9.81f, 0), new Vector3(0, ballUpSpeed, speed));
            }
        }

        public Vector3 GetAcceleration()
        {
            //The rel motion gun is not accelerated in the Kinematics scene
            return Vector3.zero;
        }

        public Vector3 GetVelocity()
        {
            return transform.forward * speed;
        }
    }

}

