using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kinematics
{
    public class BallController : MonoBehaviour
    {
        private KinematicCalc kinematicCalc = null;
        //The initial position at which the ball will be reset
        //when it enters the gun collider
        private Vector3 initPosition;

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

    }
}

