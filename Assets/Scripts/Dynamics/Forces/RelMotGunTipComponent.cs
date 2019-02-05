using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dynamics.Forces
{
    public class RelMotGunTipComponent : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Trigger"))
            {
                var controller = GetComponentInParent<RelMotGunController>();
                if (!controller.HasShoot)
                    controller.Shoot();
                controller.HasShoot = !controller.HasShoot;
            }
            else if (other.CompareTag("EndTrack"))
            {
                var controller = GetComponentInParent<RelMotGunController>();
                controller.Brake(other.transform.forward);
            }
        }
    }
}


