using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamics
{
    public class BallController : MonoBehaviour, IAcceleration, IVelocity
    {
        private Vector3 lastVelocity;
        [SerializeField]
        private float timeStep = 1f;
        private float lastTime;

        private void Start()
        {
            lastTime = Time.fixedTime;
        }

        private void FixedUpdate()
        {
            if (Time.fixedTime - lastTime >= timeStep)
            {
                lastVelocity = GetComponent<Rigidbody>().velocity;
                lastTime = Time.fixedTime;
            }
        }

        public Vector3 GetAcceleration()
        {
            var currVelocity = GetComponent<Rigidbody>().velocity;
            if (Mathf.Approximately(currVelocity.y, lastVelocity.y))
                return Vector3.zero;
            return (currVelocity - lastVelocity) / timeStep;
        }

        public Vector3 GetVelocity()
        {
            return GetComponent<Rigidbody>().velocity;
        }
    }
}


