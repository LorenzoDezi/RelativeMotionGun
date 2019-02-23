using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamics
{
    [RequireComponent(typeof(Rigidbody))]
    public class RelMotGunController : MonoBehaviour, IAcceleration, IVelocity
    {
        [SerializeField]
        private float speed = 25f;
        private bool hasShoot = false;
        public bool HasShoot
        {
            get { return hasShoot; }
            set { hasShoot = value; }
        }
        [SerializeField]
        private Rigidbody ball;
        [SerializeField]
        private float ballUpSpeed = 3f;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = speed * transform.forward;
        }

        public void Shoot()
        {
            if (ball != null)
            {
                ball.velocity = new Vector3(0, ballUpSpeed, GetComponent<Rigidbody>().velocity.z);
            }
        }

        public void Brake()
        {
            speed = -speed;
            GetComponent<Rigidbody>().velocity = speed * transform.forward;
        }

        public Vector3 GetAcceleration()
        {
            //The rel mot gun in the dynamics scene is not accelerated
            return Vector3.zero;
        }

        public Vector3 GetVelocity()
        {
            return GetComponent<Rigidbody>().velocity;
        }
    }
}


