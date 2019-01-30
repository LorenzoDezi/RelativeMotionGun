using System.Collections;
using System.Collections.Generic;
using Kinematic;
using UnityEngine;

public class KinematicBallController : MonoBehaviour
{
    private KinematicCalc kinematicCalc = null;
    //Counts the number of times it collides with the gun
    //the second time is in the falling part of the motion
    private int triggerGunCount = 0;
    //The initial position at which the ball will be reset
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
        triggerGunCount++;
        Debug.Log(triggerGunCount);
        if (triggerGunCount == 2)
        {
            kinematicCalc = null;
            transform.parent = other.transform;
            transform.localPosition = initPosition;
            triggerGunCount = 0;
        }
    }
}
