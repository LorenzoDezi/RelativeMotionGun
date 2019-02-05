using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamics.Forces
{
    [RequireComponent(typeof(Rigidbody))]
    public class RelMotGunController : MonoBehaviour
    {
        [SerializeField]
        private float accIntensity = 5f;
        [SerializeField]
        private float brakeIntensity = 2f;
        private bool hasShoot = false;
        public bool HasShoot
        {
            get { return hasShoot; }
            set { hasShoot = value; }
        }
        [SerializeField]
        private Rigidbody ball;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * accIntensity, ForceMode.Force);
        }

        private void FixedUpdate()
        {
            Debug.Log(GetComponent<Rigidbody>().velocity);
            var rigBody = GetComponent<Rigidbody>();
            rigBody.AddForce(transform.forward * rigBody.mass * accIntensity, ForceMode.Force);
        }

        public void Brake(Vector3 direction)
        {
            accIntensity = -accIntensity;
            var rigBody = GetComponent<Rigidbody>();
            rigBody.AddForce(direction * brakeIntensity * rigBody.velocity.sqrMagnitude, ForceMode.Impulse);
        }

        public void Shoot()
        {
            if (ball != null)
            {
                //TODO
                //ball.velocity = new Vector3(0, ballUpSpeed, GetComponent<Rigidbody>().velocity.z);
            }
        }

    }
}


