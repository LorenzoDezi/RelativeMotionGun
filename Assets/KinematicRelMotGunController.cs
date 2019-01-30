using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicRelMotGunController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float maxLength = 24.5f;
    [SerializeField]
    private KinematicBallController ballController;
    [SerializeField]
    private float ballUpSpeed = 3f;

    void Update()
    {
        float newZ = transform.position.z + speed * Time.deltaTime;
        if (UnityEngine.Mathf.Abs(newZ) > maxLength)
        {
            speed = -speed;
            newZ = transform.position.z + speed * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y,
            newZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger")) return;
        Destroy(other);
        Shoot();
    }

    void Shoot()
    {
        if(ballController != null)
        {
            ballController.transform.parent = null;
            ballController.Launch(new Vector3(0, -9.81f, 0), new Vector3(0, ballUpSpeed, speed));
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
