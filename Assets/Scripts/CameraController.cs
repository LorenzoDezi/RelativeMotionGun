﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform transformToFollow;
    [SerializeField]
    private float smoothSpeed = 0.0125f;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private string activateAxis;
    [SerializeField]
    private string deactivateAxis;
    [SerializeField]
    private bool isKinematic = false;

    private void Update()
    {
        if (Input.GetAxis(activateAxis) > 0)
            GetComponent<Camera>().depth = 1;
        else if (Input.GetAxis(deactivateAxis) > 0)
            GetComponent<Camera>().depth = 0;
    }

    private void FixedUpdate()
    {
        if(!isKinematic)
        {
            Vector3 desiredPosition = transformToFollow.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }

    void LateUpdate()
    {
        if (isKinematic)
        {
            Vector3 desiredPosition = transformToFollow.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}


