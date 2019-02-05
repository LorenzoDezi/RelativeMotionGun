using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Kinematics
{

    public class KinematicCalc
    {
        private List<Vector3> positions;
        private List<Vector3> speeds;
        private List<Vector3> accelerations;
        private Vector3 currentAcc;
        private int time = 0;

        public KinematicCalc(Vector3 startPos, Vector3 startSpeed, Vector3 startAcc)
        {
            positions = new List<Vector3>();
            speeds = new List<Vector3>();
            accelerations = new List<Vector3>();
            this.positions.Add(startPos);
            this.speeds.Add(startSpeed);
            this.accelerations.Add(startAcc);
            this.currentAcc = startAcc;
        }

        public Vector3 CurrentPosition => positions[time];
        public Vector3 CurrentSpeed => speeds[time];
        public Vector3 CurrentAcceleration => accelerations[time];

        public bool MoveNext(float deltaTime)
        {
            time++;
            accelerations.Add(currentAcc);
            speeds.Add(speeds[time - 1] + 1f / 2f * 
                (accelerations[time - 1] * deltaTime + accelerations[time] * deltaTime));
            positions.Add(positions[time - 1] + 1 / 2 * 
                (speeds[time - 1] * deltaTime + speeds[time] * deltaTime));
            return true;
        }

        public void Reset()
        {
            positions = new List<Vector3>() { positions[0] };
            speeds = new List<Vector3>() { speeds[0] };
            accelerations = new List<Vector3>() { accelerations[0] };
            this.currentAcc = accelerations[0];
            this.time = 0;
        }

        public void SetAcceleration(Vector3 acc)
        {
            this.currentAcc = acc;
        }
    }
}
