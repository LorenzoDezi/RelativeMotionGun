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
        private float timeWhenShoot;
        private float ballDrag = 0f;
        private Vector3 Velocity0;
        [SerializeField]
        private float friction = 0.2f;
        [SerializeField]
        private float frictionCorrection = 1.10f;

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
                //Calculation to set the proper force in order to catch the ball
                float timeFromShoot = Time.fixedTime - timeWhenShoot;
                var frictionForceCorrection = friction * frictionCorrection * 9.81f;
                var dragCorrection = 2 * Mathf.Exp(-ballDrag * timeFromShoot) * Velocity0.z / timeFromShoot;
                var velGunCorrection = 2 * Velocity0.z / timeFromShoot;
                if(currVelocity < Mathf.Abs(ball.velocity.z))
                    rigBody.AddForce(transform.forward * rigBody.mass * (dragCorrection + frictionForceCorrection 
                            - velGunCorrection), 
                        ForceMode.Force);
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
                //Velocity0 is needed for brake calculation. The same for gun and ball.
                Velocity0 = ball.velocity;
                isBraking = true;
                timeWhenShoot = Time.fixedTime;
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


