using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform holder;
    Vector3 startPoint;
    public Renderer[] fishRends;

    public float zoomMin, zoomMax;
    public float zoomAxis;
    public float currentZoom;
    Vector3 zoomPos;
    public GameObject fpc;
    public GameObject tpc;
    public static bool isFirstPerson;

    void Start()
    {
        startPoint = holder.position;
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.forward, Color.green);

        if (Physics.Raycast(ray, out hit, Mathf.Abs(currentZoom*10)))
        {
            if (hit.collider.gameObject.layer != 9)
            {
                holder.position = hit.point;
                Debug.DrawLine(ray.origin, hit.point);
            }
        }
        else
        {

            zoomAxis = Input.GetAxis("Mouse ScrollWheel");

            if (zoomAxis > 0)
            {
                currentZoom += 0.005f;
            }

            if (zoomAxis < 0)
            {
                currentZoom -= 0.005f;
            }

            if (currentZoom >= zoomMax)
            {
                currentZoom = zoomMax;
                fpc.SetActive(true);
                tpc.SetActive(false);
            }
           
            if (currentZoom <= zoomMin){
                currentZoom = zoomMin;
            }
            if(currentZoom < zoomMax)
            {
                fpc.SetActive(false);
                tpc.SetActive(true);
            }
            zoomPos = new Vector3(0, 0, currentZoom);

            holder.localPosition = zoomPos;

        }

        if (holder.localPosition.z > -0.06f)
        {
            for (int i = 0; i < fishRends.Length; i++)
            {
                fishRends[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < fishRends.Length; i++)
            {
                fishRends[i].enabled = true;
            }
        }


        if (Input.GetButtonDown("Fire3"))
        {
            isFirstPerson = !isFirstPerson;
        }

        if (isFirstPerson)
        {   fpc.SetActive(true);
            tpc.SetActive(false);
        }
        else
        {   fpc.SetActive(false);
            tpc.SetActive(true);

        }

    }
}
