using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralManagement : MonoBehaviour
{
    List <GameObject> all = new List<GameObject>();
    List<Renderer> renderers = new List<Renderer>();

    void Start()
    {
       
        Application.targetFrameRate = 60;

/*
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            all.Add(go);
        }

        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].GetComponent<Renderer>() != null)
            {
                renderers.Add(all[i].GetComponent<Renderer>());
            }
        }*/
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }
}
