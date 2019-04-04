using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CausticAnimation : MonoBehaviour
{
    public Material myMat;
    public Material[] mats;
    public int counter;
    
    void Update()
    {
        counter++;
        if(counter >= mats.Length)
        {
            counter = 0;
        }

        myMat = mats[counter];
    }
}
