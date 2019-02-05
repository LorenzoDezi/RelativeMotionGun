using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinematics
{
    public class RelMotGunController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5f;
        private float maxLength = 24.5f;
        [SerializeField]
        private BallController ballController;
        [SerializeField]
        private float ballUpSpeed = 3f;
        private bool hasShoot = false;

        void Update()
        {
            float newZ = transform.position.z + speed * Time.deltaTime;
            if (UnityEngine.Mathf.Abs(newZ) > maxLength)
            {
                speed = -speed;
                newZ = transform.position.z + speed * Time.deltaTime;
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

        public float GetSpeed()
        {
            return speed;
        }
    }

}

