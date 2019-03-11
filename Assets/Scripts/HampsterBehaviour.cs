using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HampsterBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public Transform lookPivot;
    public Transform lookAtSpot;
    public Transform rayPoint;
    public GameObject ball;
    public float lookSpeed;
    public float rotateSpeed;
    public float speed;
    public float randyX;
    public float randyY;
    bool atSurface;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }


    void Update()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (!atSurface)
            {
                rb.useGravity = false;
                rb.drag = 10;
            }
            ball.transform.Rotate(rotateSpeed, 0, 0);
            rb.AddRelativeForce(new Vector3(0, 0, speed * Time.deltaTime));
        }

        float rand = Random.Range(0, 100);
        lookPivot.position = transform.position;
        if (rand > 98)
        {
            ChangeRotation();
        }
        
        Quaternion OriginalRot = transform.rotation;
        transform.LookAt(lookAtSpot);
        Quaternion NewRot = transform.rotation;
        transform.rotation = OriginalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, lookSpeed * Time.deltaTime);

        Ray ray = new Ray(rayPoint.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(rayPoint.position, transform.forward, Color.green, 2);

        if (Physics.Raycast(ray, out hit, 2f))
        {
            speed = 0;
        }
        else
            speed = 80;

            lookPivot.localRotation = Quaternion.Euler(randyX * 90, randyY * 90, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Surface")
        {
           atSurface = true;
           rb.useGravity = true;
           rb.drag = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Surface")
        {
            atSurface = false;
        }
    }

    void ChangeRotation()
    {
      randyY = Random.Range(0.5f, 4f);
      randyX = Random.Range(0.5f, 4f);
    }
}
