using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamics.Forces
{
    [RequireComponent(typeof(Rigidbody))]
    public class RelMotGunController : MonoBehaviour, IAcceleration, IVelocity
    {
        [SerializeField]
        private float accIntensity = 5f;
        [SerializeField]
        private float brakeIntensity = 2f;
        [SerializeField]
        private float shootUpIntensity = 10f;
        [SerializeField]
        private float maxVelocity = 2f;
        [SerializeField]
        private float drag = 1.5f;
        private bool hasShoot = false;
        public bool HasShoot
        {
            get { return hasShoot; }
            set { hasShoot = value; }
        }
        [SerializeField]
        private Rigidbody ball;
        [SerializeField]
        private Vector3 centerOfMass;
        private bool isBraking = false;

        private float ballDrag = 0f;
        [SerializeField]
        private float friction = 0.2f;
        [SerializeField]
        private float frictionCorrection = 1.25f;

        private Vector3 lastVelocity;
        [SerializeField]
        private float timeStep = 1f;
        private float lastTime;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * accIntensity, ForceMode.Force);
            GetComponent<Rigidbody>().centerOfMass = centerOfMass;
            lastVelocity = GetComponent<Rigidbody>().velocity;
            lastTime = Time.fixedTime;
        }

        private void FixedUpdate()
        {
            var rigBody = GetComponent<Rigidbody>();
            //Check for the last velocity value after the timestep
            if (Time.fixedTime - lastTime >= timeStep)
            {
                lastVelocity = rigBody.velocity;
                lastTime = Time.fixedTime;
            }
            var currVelocity = Mathf.Abs(rigBody.velocity.z);
            if (isBraking)
            {
                var frictionForceCorrection = friction * frictionCorrection * rigBody.mass * 9.81f;
                rigBody.AddForce(transform.forward * (drag * (rigBody.velocity.z) * ball.mass + frictionForceCorrection), ForceMode.Force);
            }
            else if (currVelocity < maxVelocity)
                rigBody.AddForce(transform.forward * rigBody.mass * accIntensity, ForceMode.Force);
        }

        public Vector3 GetAcceleration()
        {
            var currVelocity = GetComponent<Rigidbody>().velocity;
            if (Mathf.Approximately(currVelocity.z, lastVelocity.z))
                return Vector3.zero;
            return (currVelocity - lastVelocity) / timeStep;
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
                Rigidbody rigBody = GetComponent<Rigidbody>();
                ballDrag = ball.drag;
                isBraking = true;
                ball.AddForce(Vector3.up * shootUpIntensity, ForceMode.Impulse);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball") && isBraking)
                isBraking = false;
        }

        public Vector3 GetVelocity()
        {
            return GetComponent<Rigidbody>().velocity;
        }
    }
}


