using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamBoard : MonoBehaviour
{

    public bool isMoon;
    public Transform objectStartPoint;
    public CanYouGetThis[] getThisScript;
    public GameObject[] interactables;
    public static List<bool> onMoon = new List<bool>();
    public static List<Vector3> intersPos = new List<Vector3>();
    public static List<Quaternion> intersRot = new List<Quaternion>();
    public static List<bool> questBools = new List<bool>();
    public static bool loaded = false;

    void Awake()
    {
        interactables = GameObject.FindGameObjectsWithTag("Grabable");

        if (!loaded)
        {
            for(int i = 0; i < getThisScript.Length; i++)
            {
                questBools.Add(false);
            }
            for (int i = 0; i < interactables.Length; i++)
            {
                intersPos.Add(interactables[i].transform.localPosition);
                intersRot.Add(interactables[i].transform.rotation);
                onMoon.Add(false);
            }

            loaded = true;
        }       

        for (int i = 0; i < getThisScript.Length; i++)
        {
            getThisScript[i].finished = questBools[i];
        }

        for (int i = 0; i < interactables.Length; i++)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (onMoon[i])
                {
                    interactables[i].SetActive(true);
                }
                else
                    interactables[i].SetActive(false);

                Physics.gravity = new Vector3(0, -1.4f, 0);
            }


            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (!onMoon[i])
                {
                    interactables[i].SetActive(true);
                }
                else
                    interactables[i].SetActive(false);

                Physics.gravity = new Vector3(0, -9.8f, 0);
            }

            interactables[i].transform.position = intersPos[i];
            interactables[i].transform.rotation = intersRot[i];

            if(interactables[i].name == PortalBehavior.heldName)
            {
                interactables[i].SetActive(true);
                interactables[i].transform.position = objectStartPoint.position;
                FixedJoint newJoint = interactables[i].AddComponent<FixedJoint>();
                newJoint.connectedBody = objectStartPoint.parent.gameObject.GetComponent<Rigidbody>();
            }
        }

    }

    void Update()
    {
        for (int i = 0; i < getThisScript.Length; i++)
        {
            if(getThisScript[i].finished)
            questBools[i] = true;
        }

        for (int i = 0; i < interactables.Length; i++)
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            onMoon[i] = interactables[i].activeInHierarchy;

            intersPos[i] = interactables[i].transform.position;
            intersRot[i] = interactables[i].transform.rotation;
        }

    }
}
