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
            col.enabled = false;
            sound.Play();
            FishGrab.canHold = false;
            inUse = true;
            other.gameObject.transform.position = tapeSpot.position;
            other.gameObject.transform.rotation = tapeSpot.rotation;
            if (other.gameObject.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject.GetComponent<Rigidbody>());
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
        Rigidbody newRB = tapeAudio.gameObject.AddComponent<Rigidbody>();
        newRB.mass = 0.3f;
        newRB.drag = 10;
        newRB.angularDrag = 5;
        tapeAudio.gameObject.tag = "Grabable";
        tapeAudio.gameObject.layer = 0;
        newRB.AddForce(transform.up * 200 + transform.right * 200);
        tapeAudio.Stop();
        tapeAudio = null;
        Invoke("ColliderReset", 0.5f);

    }
    void ColliderReset()
    {
        col.enabled = true;
    }

      
}
