using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouGetThis : MonoBehaviour
{

    GameObject player;
    Animator anim;
    public GameObject correctObject;
    public Transform correctSpot;
    public GameObject correctText;

    public float grabDistance;
    public string questionText;
    public string wrongText;
    public string thanksText;

    float speed = 0;

    public bool finished;
    float playerDistance;

    void Start()
    {
        player = GameObject.Find("Fin");
        speed = 0;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerDistance < grabDistance && !FishGrab.holding) {
            if(!finished)
                correctText.GetComponent<TextMesh>().text = questionText;
        }

        if (playerDistance < grabDistance && FishGrab.holding && FishGrab.heldObject == correctObject && !finished)
        {
            finished = true;
            FishGrab.canHold = false;
        }

        if (playerDistance < grabDistance && FishGrab.holding && FishGrab.heldObject != correctObject && !finished)
        {
            correctText.GetComponent<TextMesh>().text = wrongText;
            correctText.SetActive(true);
        }

        if (playerDistance > grabDistance)
        {
            correctText.SetActive(false);
        }
        else
        {
            correctText.SetActive(true);
        }


        if (finished)
        {
            if(correctObject.transform.position != correctSpot.position)
                speed = speed + 0.008f;

            if (correctObject.GetComponent<Rigidbody>() != null)
                correctObject.GetComponent<Rigidbody>().isKinematic = true;
            if (correctObject.GetComponent<PopUpText>() != null)
            {
                correctObject.GetComponent<PopUpText>().popUpText.SetActive(false);
                correctObject.GetComponent<PopUpText>().enabled = false;
            }

            correctObject.transform.position = Vector3.Lerp(correctObject.transform.position, correctSpot.position, speed);
            correctObject.transform.rotation = Quaternion.Lerp(correctObject.transform.rotation, correctSpot.rotation, speed);
            correctObject.transform.parent = transform;           
            correctObject.tag = "Untagged";
            correctText.GetComponent<TextMesh>().text = thanksText;
        }
        if(anim != null)
        {
            anim.SetBool("Move", correctText.activeInHierarchy);
        }
    }
}
