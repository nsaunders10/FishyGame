using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouGetThis : MonoBehaviour
{

    GameObject player;
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

            //correctObject.GetComponent<Collider>().enabled = false;
            if(correctObject.GetComponent<Rigidbody>() != null)
            correctObject.GetComponent<Rigidbody>().isKinematic = true;
            correctObject.transform.position = Vector3.Lerp(correctObject.transform.position, correctSpot.position, speed);
            correctObject.transform.rotation = Quaternion.Lerp(correctObject.transform.rotation, correctSpot.rotation, speed);
            correctObject.tag = "Untagged";
            correctText.GetComponent<TextMesh>().text = thanksText;
        }
    }
}
