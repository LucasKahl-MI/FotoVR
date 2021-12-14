using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Camera))]
public class SnapCam : MonoBehaviour
{
    Camera snapCam;

    int resWidth = 3000;
    int resHeight = 2000;

    private bool takeImage = false;

    void Awake()
    {
        Debug.Log("Camera Awake");
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
        snapCam.usePhysicalProperties = true;
    }

    public void CallTakeSnapshot()
    {
        Debug.Log("Camera Eingeschaltet");
        // Ist der Befehl, um die Kamera einzuschalten
        snapCam.gameObject.SetActive(true);
        takeImage = true;
    }

    public void CameraPositionUpdate()
    {
        GameObject g = GameObject.FindGameObjectWithTag("rightController");
        //transform.position = g.transform.position;
        snapCam.transform.parent.position = g.transform.position;
        //transform.rotation = g.transform.rotation;
        snapCam.transform.parent.rotation = g.transform.rotation;
        // snapCam.transform.parent.rotation.Set(snapCam.transform.parent.rotation.x + 90, 0, 0, 0);
    }

    void LateUpdate()
    {
        Debug.Log("Camera Update!");
        if (snapCam.gameObject.activeInHierarchy)
        {
            if (takeImage)
            {
                Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
                snapCam.Render();
                RenderTexture.active = snapCam.targetTexture;
                snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
                byte[] bytes = snapshot.EncodeToPNG();
                string fileName = SnapshotName();
                System.IO.File.WriteAllBytes(fileName, bytes);
                Debug.Log("Snapshop taken!");
                // schaltet die Kamera wieder aus
                //snapCam.gameObject.SetActive(false);
                takeImage = false;
            }
           
        }
    }

    string SnapshotName()
    {
        Debug.Log("Camera Speicher!");
        return string.Format("{0}/TakenImages/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void changeIso(int iso)
    {
          snapCam.GetComponent<HDAdditionalCameraData>().physicalParameters.iso = iso;
    }

    public void changeShutterSpeed(float shutterSpeed)
    {
        Debug.Log("shutterSpeed Change Erfolgreich 1111!!!!");
        snapCam.GetComponent<HDAdditionalCameraData>().physicalParameters.shutterSpeed = shutterSpeed;
    }

    public void changeApature(float apature)
    {
        snapCam.GetComponent<HDAdditionalCameraData>().physicalParameters.aperture = apature;
    }

    public void changeFocalLength(float brennweite)
    {
        snapCam.focalLength = brennweite;
        Debug.Log("Brennweite geänder");
    }
}
