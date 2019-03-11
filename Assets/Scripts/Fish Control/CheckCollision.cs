using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{

    public bool isColliding;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        isColliding = false;
    }
}
