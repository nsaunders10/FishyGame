using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouGetThis : MonoBehaviour
{

    GameObject player;
    Animator anim;
    Camera mainCam;
    public int questNumber;
    public GameObject correctObject;
    public Transform correctSpot;
    public GameObject correctText;
    public Transform cameraSpot;
    DreamBoard dreamBoard;

    public float grabDistance;
    public string questionText;
    public string wrongText;
    public string thanksText;

    public float speed = 0;

    public bool focused;
    public bool finished;
    float playerDistance;
    public static bool inChat;
    bool timedOut = true;
    bool leftZone = true;

    void Start()
    {
        speed = 2f;
        player = GameObject.Find("Fin");
        anim = GetComponent<Animator>();
        dreamBoard = GameObject.Find("DreamBoard").GetComponent<DreamBoard>();
        mainCam = Camera.main;

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
            focused = false;
            inChat = false;
        }
        else
        {
            correctText.SetActive(true);
            focused = true;
            inChat = true;
        }


        if (finished)
        {
            if (correctObject.GetComponent<Rigidbody>() != null)
                correctObject.GetComponent<Rigidbody>().isKinematic = true;
            if (correctObject.GetComponent<PopUpText>() != null)
            {
                correctObject.GetComponent<PopUpText>().popUpText.SetActive(false);
                correctObject.GetComponent<PopUpText>().enabled = false;
            }

            correctObject.transform.position = Vector3.Lerp(correctObject.transform.position, correctSpot.position, speed * Time.deltaTime);
            correctObject.transform.rotation = Quaternion.Lerp(correctObject.transform.rotation, correctSpot.rotation, speed * Time.deltaTime);
            correctObject.transform.parent = transform;           
            correctObject.tag = "Untagged";
            correctText.GetComponent<TextMesh>().text = thanksText;
        }
        if(anim != null)
        {
            anim.SetBool("Move", correctText.activeInHierarchy);
        }

        if (focused)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, cameraSpot.position, speed * Time.deltaTime);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, cameraSpot.rotation, speed * Time.deltaTime);

            for (int i = 0; i < dreamBoard.getThisScript.Length; i++)
            {
                if (i != questNumber)
                {
                    dreamBoard.getThisScript[i].enabled = false;
                }
            }
            timedOut = false;
            leftZone = false;
        }
        if (!focused && !timedOut)
        {
             mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, mainCam.transform.parent.transform.position, speed * Time.deltaTime);
             mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, mainCam.transform.parent.transform.rotation, speed * Time.deltaTime);
            //mainCam.transform.position = mainCam.transform.parent.transform.position;
            //mainCam.transform.rotation = mainCam.transform.parent.transform.rotation;

            for (int i = 0; i < dreamBoard.getThisScript.Length; i++)
            {
                 dreamBoard.getThisScript[i].enabled = true;
            }
            if (!leftZone)
            {
                leftZone = true;
                Invoke("StopLerp", 2f);
            }

        }

    }
    void StopLerp()
    {
        mainCam.transform.position = mainCam.transform.parent.transform.position;
        mainCam.transform.rotation = mainCam.transform.parent.transform.rotation;
        timedOut = true;
    }
}
