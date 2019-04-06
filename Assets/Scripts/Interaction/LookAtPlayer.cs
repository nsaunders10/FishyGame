using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform player;
    Transform lookingAtPlayer;
    public bool useOneAxis;
    public float lookSpeed = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lookingAtPlayer = transform;
    }

    
    void Update()
    {
        if (useOneAxis)
        {
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(player);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            NewRot.z = 0;
            NewRot.x = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, lookSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(player);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, lookSpeed * Time.deltaTime);
        }
    }
}
