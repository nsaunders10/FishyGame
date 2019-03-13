using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishGrab : MonoBehaviour
{
    
    GameObject hitObj = null;
    Rigidbody objRB;
    Rigidbody rb;
    AudioSource sound;
    public static bool canHold = true;
    public static bool holding = false;
    public static GameObject heldObject;
    bool jointGrabbed;
    public Transform objPoint;
    GameObject jointObjHit;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();

        if (PortalBehavior.broughtObj)
        {
            if (transform.Find(PortalBehavior.heldName).gameObject != null)
            {  
                hitObj = transform.Find(PortalBehavior.heldName).gameObject;
                objRB = hitObj.GetComponent<Rigidbody>();
                Destroy(objRB);
                objRB.isKinematic = true;
                hitObj.transform.parent = this.transform;
                holding = true;
                hitObj.layer = 2;
            }
        }
    }


    void Update()
    {

        heldObject = hitObj;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Input.GetButton("Fire1") && canHold && !holding)
        {
            if (Physics.Raycast(ray, out hit, 0.2f))
            {
                if (hit.collider.tag == "Grabable")
                {
                    hitObj = hit.collider.gameObject;
                    sound.pitch = 2;
                    sound.Play();
                    objRB = hitObj.GetComponent<Rigidbody>();
                    Destroy(objRB);
                    objRB.isKinematic = true;
                    hitObj.transform.parent = this.transform;
                    holding = true;
                    hitObj.layer = 2;
                }

                if (hit.collider.tag == "Joint Grab")
                {
                    jointGrabbed = true;
                    jointObjHit = hit.collider.gameObject;
                    sound.pitch = 2;
                    sound.Play();
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (hitObj != null)
            {
                if (hitObj.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody newRB = hitObj.AddComponent<Rigidbody>();
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        newRB.mass = 0.3f;
                        newRB.drag = 0;
                        newRB.angularDrag = 0;
                    }
                    else
                    {
                        newRB.mass = 0.3f;
                        newRB.drag = 10;
                        newRB.angularDrag = 5;
                    }
                    newRB.velocity = rb.velocity;
                    hitObj.layer = 0;
                    hitObj.transform.parent = null;
                    hitObj = null;
                    sound.pitch = 1;
                    sound.Play();
                }
            }
            canHold = true;
            holding = false;
            jointGrabbed = false;
            PortalBehavior.broughtObj = false;
            if(jointObjHit != null)
            {
                jointObjHit.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        if (!canHold && hitObj != null && hitObj.tag != "Joint Grab")
        {
            sound.pitch = 1;
            sound.Play();
            holding = false;
            hitObj.transform.parent = null;
            hitObj = null;
        }

        if (jointGrabbed)
        {
            JointGrab();
        }
        else
        {
            jointObjHit = null;
        }
    }

    void JointGrab()
    {
        jointObjHit.GetComponent<Rigidbody>().position = objPoint.position;
        jointObjHit.GetComponent<Rigidbody>().isKinematic = true;
        holding = true;
        jointObjHit.layer = 2;
    }
}
