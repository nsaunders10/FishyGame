using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    GameObject player;
    public float popUpDistance;
    public GameObject popUpText;
    public string message;
    
    void Start()
    {
        player = GameObject.Find("Fin");
        popUpText.GetComponent<TextMesh>().text = message;
    }

    
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance < popUpDistance)
        {
            popUpText.SetActive(true);
        }
        else
            popUpText.SetActive(false);

    }
}
