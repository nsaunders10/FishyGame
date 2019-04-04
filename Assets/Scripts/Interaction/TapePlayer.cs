using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    AudioSource tapeAudio;
    Collider col;
    public Transform tapeSpot;
    bool inUse;
    AudioSource sound;

    void Start()
    {
       sound = GetComponent<AudioSource>();
       col = GetComponent<Collider>();
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Substring(0,3) == "Tap" && !inUse && other.tag == "Grabable")
        {
            sound.Play();
            FishGrab.canHold = false;
            inUse = true;
            other.gameObject.transform.position = tapeSpot.position;
            other.gameObject.transform.rotation = tapeSpot.rotation;

            if (other.GetComponents<FixedJoint>() != null)
            {
                Rigidbody tapeRB = other.GetComponent<Rigidbody>();
                tapeRB.isKinematic = true;

            }
            tapeAudio = other.GetComponent<AudioSource>();
            other.gameObject.tag = "Untagged";
            Invoke("PlayDelay", 0.5f);
        }
    }
    void PlayDelay()
    {
        tapeAudio.Play();

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButtonDown("Fire1") && inUse)
        {
            inUse = false;
            Invoke("EjectDelay", 0.5f);
            sound.Play();
        }

    }

    void EjectDelay()
    {
        Rigidbody tapeRB = tapeAudio.gameObject.GetComponent<Rigidbody>();
        tapeRB.isKinematic = false;
        tapeRB.useGravity = true;
        tapeRB.drag = 10;
        tapeAudio.gameObject.tag = "Grabable";
        tapeAudio.gameObject.layer = 0;
        tapeRB.AddForce(transform.up * 200 + transform.right * 200);
        tapeAudio.Stop();
        tapeAudio = null;
    }     
}
