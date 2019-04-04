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
    public Transform objPoint;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();

        if (PortalBehavior.broughtObj)
        {
            if (GameObject.Find(PortalBehavior.heldName).gameObject != null)
            {  
                hitObj = GameObject.Find(PortalBehavior.heldName).gameObject;
                hitObj.GetComponent<Rigidbody>().useGravity = false;
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
                if (hit.collider.tag == "Joint Grab" || hit.collider.tag == "Grabable")
                {
                    holding = true;
                    hitObj = hit.collider.gameObject;
                    FixedJoint newJoint = hitObj.AddComponent<FixedJoint>();
                    newJoint.connectedBody = rb;
                    hitObj.GetComponent<Rigidbody>().useGravity = false;
                    hitObj.GetComponent<Rigidbody>().drag = 0;
                    hitObj.layer = 2;
                    sound.pitch = 2;
                    sound.Play();
                }
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (hitObj != null)
            {
                FixedJoint[] newJoint = hitObj.GetComponents<FixedJoint>();
                if (newJoint.Length == 2)
                    Destroy(newJoint[newJoint.Length - 1]);
                else
                    Destroy(newJoint[0]);
                hitObj.GetComponent<Rigidbody>().useGravity = true;
                hitObj.GetComponent<Rigidbody>().drag = 10;
                holding = false;
                sound.pitch = 1;
                sound.Play();
                hitObj.layer = 0;
                hitObj = null;
            }
            canHold = true;
            holding = false;
            PortalBehavior.broughtObj = false;       
        }

        if (!canHold && hitObj != null)
        {
            if (hitObj.GetComponents<FixedJoint>()!=null)
            {
                FixedJoint[] newJoint = hitObj.GetComponents<FixedJoint>();
                if (newJoint.Length > 1)
                    Destroy(newJoint[newJoint.Length - 1]);
                else
                    Destroy(newJoint[0]);
            }
            sound.pitch = 1;
            sound.Play();
            holding = false;
            hitObj.transform.parent = null;
            hitObj = null;
        }
    }
}
