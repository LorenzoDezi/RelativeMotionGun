using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DynamicRelMotGunController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float maxLength = 24.5f;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity);
        if (Mathf.Abs(transform.position.z) > maxLength)
        {
            speed = -speed;
        }
        GetComponent<Rigidbody>().velocity = speed * transform.forward;
    }
}
