using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DynamicRelMotGunController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float maxLength = 18f;
    private bool hasShoot = false;
    public bool HasShoot {
        get { return hasShoot; }
        set { hasShoot = value; }
    }
    [SerializeField]
    private Rigidbody ball;
    [SerializeField]
    private float ballUpSpeed = 3f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = speed * transform.forward;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.z) > maxLength)
        {
            speed = -speed;
            GetComponent<Rigidbody>().velocity = speed * transform.forward;
        }
    }

    public void Shoot()
    {
        if (ball != null)
        {
            ball.velocity = new Vector3(0, 5*ballUpSpeed, GetComponent<Rigidbody>().velocity.z);
        }
    }

}
