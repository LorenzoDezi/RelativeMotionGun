using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelMotGunTipComponent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;
        var controller = GetComponentInParent<DynamicRelMotGunController>();
        if (!controller.HasShoot)
            controller.Shoot();
        controller.HasShoot = !controller.HasShoot;
    }
}
