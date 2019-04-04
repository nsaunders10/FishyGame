using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{   float waterLevel = 4;
    public float floatHeight = 2;
    public float bounceDamp = 0.05f;
    public Vector3 buoyancyCenterOffset;
    public Transform surface;

    float forceFactor;
    Vector3 actionPoint;
    Vector3 upLift;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        waterLevel = surface.position.y + floatHeight;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            actionPoint = transform.position + transform.TransformDirection(buoyancyCenterOffset);
            forceFactor = 1 - ((actionPoint.y - waterLevel));

            rb.mass = 0.3f;
            rb.drag = 1;
            rb.angularDrag = 0.5f;

            if (forceFactor > 0)
            {
                upLift = (-Physics.gravity/2) * (forceFactor - rb.velocity.y * bounceDamp);
                rb.AddForceAtPosition(upLift, actionPoint);
            }
        }
    }
}
