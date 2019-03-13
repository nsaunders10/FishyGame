using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PortalBehavior : MonoBehaviour
{
    public Collider pictureCollider;
    public GameObject cameraHolder;
    public Transform camPos;
    public FishController fishCon;
    public FishGrab fishGrab;
    public Animator camAnim;
    public PostProcessVolume postFX;
    DepthOfField dof;
    Vignette vignette;
    public int sceneNumber;
    float speed;
    bool entered;
    public static string heldName;
    public static bool broughtObj;
   
    void Start()
    {
        postFX.profile.TryGetSettings(out dof);
        postFX.profile.TryGetSettings(out vignette);
    }
   
    void Update()
    {
        CameraCollision camCol = cameraHolder.GetComponent<CameraCollision>();
        if (entered)
        {
            camAnim.enabled = true;
            if(camCol.currentZoom > -0.163f)
            {
                camCol.currentZoom -= 0.001f;
            }

            if (camCol.currentZoom < -0.163f)
            {
                camCol.currentZoom += 0.001f;
            }
            speed = speed + 0.0025f;
            camPos.localPosition = new Vector3(0, 0, 0 + speed/2);
            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, camPos.position, speed);
            cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, camPos.rotation, speed);
            dof.active = false;
            vignette.active = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pictureCollider.isTrigger = false;
            fishCon.enabled = false;
            fishGrab.enabled = false;
            entered = true;
            CameraCollision.isFirstPerson = false;
            Invoke("LoadDelay", 3);

            if (FishGrab.heldObject != null)
            {
                heldName = FishGrab.heldObject.name;
                broughtObj = true;
            }
            else
            {
                heldName = null;
                broughtObj = false;
            }
        }
    }

    void LoadDelay()
    {
        StartCoroutine(LoadOtherScene());
    }

    IEnumerator LoadOtherScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber);

        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
