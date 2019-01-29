using System.Collections;
using System.Collections.Generic;
using Kinematic;
using UnityEngine;

public class KinematicBallController : MonoBehaviour
{
    private KinematicCalc kinematicCalc = null;
    [SerializeField]
    private float step = 0.1f;

    public void Launch(Vector3 acceleration, Vector3 speed)
    {
        kinematicCalc = new KinematicCalc(transform.position,
           speed, acceleration
            );
    }

    // Update is called once per frame
    void Update()
    {
        if (kinematicCalc != null && kinematicCalc.MoveNext(Time.deltaTime))
        {
            transform.position += kinematicCalc.CurrentSpeed * Time.deltaTime;
        }
    }
}
