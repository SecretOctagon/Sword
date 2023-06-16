using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform followTarget;
    [SerializeField] float maxSpeed;
    [SerializeField] float smoothing;
    Vector3 velocity;
    void Start()
    {
        if (!cam)
            cam = GetComponentInChildren<Camera>();
        cam.transform.LookAt(transform);
    }

    void Update()
    {
        if (followTarget)
        {
            Vector3 position = Vector3.SmoothDamp(transform.position, followTarget.position, ref velocity, smoothing, maxSpeed);
            transform.position = position;
        }
    }
}
