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
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
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
                if (hit.collider.tag == "Grabable" && hit.collider.gameObject.layer != 9)
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
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (hitObj != null)
            {
                Rigidbody newRB = hitObj.AddComponent<Rigidbody>();
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {   newRB.mass = 0.3f;
                    newRB.drag = 0;
                    newRB.angularDrag = 0;
                } else
                {   newRB.mass = 0.3f;
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
            canHold = true;
            holding = false;
        }

        if (!canHold && hitObj != null)
        {
            sound.pitch = 1;
            sound.Play();
            holding = false;
            hitObj.transform.parent = null;
            hitObj = null;
        }
    }
}
