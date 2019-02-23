using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinematics
{
    public class BallController : MonoBehaviour, IAcceleration, IVelocity
    {
        private KinematicCalc kinematicCalc = null;
        //The initial position at which the ball will be reset
        //when it enters the gun collider
        private Vector3 initPosition;
        //Used only for interface need
        private Vector3 forwardSpeed;

        public void Launch(Vector3 acceleration, Vector3 speed)
        {
            kinematicCalc = new KinematicCalc(transform.position,
               speed, acceleration
                );
        }

        private void Start()
        {
            initPosition = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (kinematicCalc != null && kinematicCalc.MoveNext(Time.deltaTime))
            {
                transform.position += kinematicCalc.CurrentSpeed * Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Gun")) return;
            kinematicCalc = null;
            transform.parent = other.transform;
            transform.localPosition = initPosition;
        }

        public Vector3 GetAcceleration()
        {
            if (kinematicCalc != null)
                return kinematicCalc.CurrentAcceleration;
            else
                return Vector3.zero;
        }

        public Vector3 GetVelocity()
        {
            if (kinematicCalc != null)
                return kinematicCalc.CurrentSpeed;
            else
                return forwardSpeed;
        }

        /// <summary>
        /// The forward speed is set by the relative motion gun,
        /// it is necessary only for the GetVelocity() method.
        /// </summary>
        /// <param name="forwardSpeed"></param>
        public void SetForwardSpeed(Vector3 forwardSpeed)
        {
            this.forwardSpeed = forwardSpeed;
        }
    }
}

