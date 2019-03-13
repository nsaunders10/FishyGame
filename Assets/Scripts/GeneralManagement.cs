using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralManagement : MonoBehaviour
{
    List <GameObject> all = new List<GameObject>();
    List<Renderer> renderers = new List<Renderer>();
    public Text fpsText;
    float min = 60;
    float max = 60;

    void Start()
    {
       
        Application.targetFrameRate = 60;


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
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            max = 60;
            min = 60;
        }

        if(Mathf.RoundToInt(1.0f / Time.deltaTime) > max){
            max = Mathf.RoundToInt(1.0f / Time.deltaTime);
        }

        if (Mathf.RoundToInt(1.0f / Time.deltaTime) < min)
        {
            min = Mathf.RoundToInt(1.0f / Time.deltaTime);
        }

        fpsText.text = ""+ Mathf.RoundToInt(1.0f / Time.deltaTime) + " : " + min + " : " + max;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        for (int i = 0; i < renderers.Count; i++)
        {
            if (renderers[i].isVisible)
            {
                //renderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                //renderers[i].enabled = true;
            }
            else
            {
                // renderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
               // renderers[i].enabled = false;
            }
        }
        }
}
