using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    public float speed;
    public float lookSpeed;
    public float sensitivity;
    public Transform cameraHolder;
    public Transform lookPivot;
    public Transform lookAtSpot;
    public GameObject bubbles;
    Rigidbody rb;

    float rotX;
    float rotY;
    float totalInput;
    float x;
    float y;
    float mouseX;
    float mouseY;
    bool canBoost = true;
    bool atSurface;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update(){
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;


        Cursor.lockState = CursorLockMode.Locked;

        cameraHolder.transform.position = transform.position;

        if (Input.GetButton("Fire2") && canBoost)
        {
            rb.AddRelativeForce(new Vector3(0, 0, totalInput * speed * Time.deltaTime * 3f));
            Invoke("Boost", 0.3f);
            bubbles.SetActive(true);
        }
        
        if(y >= 0)
         lookPivot.localRotation = Quaternion.Euler(0, x*90, 0);
        else
         lookPivot.localRotation = Quaternion.Euler(180, -x *90, 0);


        //Lerp LookAt
        if (y != 0 || x != 0 || Input.GetButton("Fire1") || CameraCollision.isFirstPerson)
        {
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(lookAtSpot);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, lookSpeed * Time.deltaTime);
        }

        totalInput = Mathf.Abs(x) + Mathf.Abs(y);
        if (totalInput > 1)
            totalInput = 1;

        cameraHolder.Rotate(mouseY, mouseX, 0);

        rotX = cameraHolder.rotation.eulerAngles.x;
        rotY = cameraHolder.rotation.eulerAngles.y;

        if (rotX < 90 && rotX > 75)
        {
            rotX = 75;
        }
        else
        if (rotX < 272 && rotX > 270)
        {
            rotX = 272;
        }


        if(rotX < 280 && rotX > 80)
        {
            sensitivity = 8;
        }else
        {
            sensitivity = 40;
        }

        Vector3 rot = new Vector3(rotX, rotY, 0);
        cameraHolder.rotation = Quaternion.Euler(rot);

        if (atSurface)
        {
            rb.drag = 0;
            rb.useGravity = true;
        }
        else
        {
            rb.drag = 1.8f;
            rb.useGravity = false;
        }

    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(0, 0, totalInput * speed * Time.deltaTime));

    }
    private void LateUpdate()
    {
        atSurface = false;
    }

    void Boost()
    {
        CancelInvoke("Boost");
        canBoost = false;
        Invoke("ResetBoost", 2f);
    }
    void ResetBoost()
    {
        bubbles.SetActive(false);
        canBoost = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Surface")
        {
            atSurface = true;
            rb.drag = 0;
            rb.useGravity = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Surface")
        {

            rb.drag = 1.8f;
            rb.useGravity = false;
        }
    }

}
