using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputConsumer : MonoBehaviour
{

    public SnapCam snapCam;
    private bool bildGemacht = false;
    private bool gripButton = false;
    private bool joyPress = false;
    private float brennweite = 35.0f;
    private bool change = false;
    private int iso = 100;
    private int isoMax = 12800;
    private int isoMin = 100;
    private float verschlussZeit = 0.005f;
    //private List<float> verschlusszeitWerte = new List<float> { 1.0f, 1.3f, 1.6f, 2.0f, 2.5f, 3.0f, 4.0f, 5.0f, 6.0f, 8.0f, 10.0f, 13.0f, 15.0f, 20.0f, 25.0f, 30.0f, 40.0f, 50.0f, 60.0f, 80.0f, 100.0f, 125.0f, 160.0f, 200.0f, 250.0f, 320.0f, 400.0f, 500.0f, 640.0f, 800.0f, 1000.0f, 1250.0f, 1600.0f, 2000.0f };
    private List<float> verschlusszeitWerte = new List<float> { 1.0f, 0.5f, 0.3f, 0.25f, 0.2f, 0.16f, 0.125f, 0.1f, 0.07f, 0.067f, 0.05f, 0.04f, 0.033f, 0.025f, 0.02f, 0.0167f, 0.0125f, 0.01f, 0.008f, 0.00625f, 0.005f, 0.004f, 0.003125f, 0.0025f, 0.002f, 0.0015625f, 0.00125f, 0.001f, 0.0008f, 0.000625f, 0.0005f };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        snapCam.CameraPositionUpdate();
    }

    // Foto aufnehmen
    void OnTriggerButton(bool b)
    {

        /*
        if (b == bildGemacht) return; 
        if (b) Debug.Log("foto");
        bildGemacht = b;
        */

        if (b == true && bildGemacht == false)
        {
            Debug.Log("foto");
            snapCam.CallTakeSnapshot();
            bildGemacht = true;
        }
        else if (b == false && bildGemacht == true)
        {
            bildGemacht = false;
        }
    }

    void OnMenuButton(bool b)
    {

        if (b)
        {
            Debug.Log(b + "Menu aufgerufen");
        }
    }


    // ISO und Verschlusszeit verändern
    void OnPrimary2DAxis(Vector2 a)
    {
        // if (!joyPress) return;

        int dir = 0;    // 0,1 = left, right - 2,3 = up, down 

        float val = a.x;
        if (Mathf.Abs(a.x) > Mathf.Abs(a.y))
        {
            val = a.y;
            dir += 2;
        }

        if (val < 0) dir++;

        switch (dir)
        {
            case 0:
                if (joyPress && !change)
                {
                    Debug.Log("Iso größer");
                    //if (gripButton)
                    //{
                      //  Debug.Log("Caro funktioniert auch hier");
                    //}
                    
                    if (this.iso < isoMax)
                    {
                        this.iso = this.iso * 2;
                        snapCam.changeIso(this.iso);
                        change = true;
                    }
                }
                else if (!joyPress && change)
                {
                    change = false;
                }
                break;
            case 1:
                if (joyPress && !change)
                {
                    Debug.Log("Iso kleiner");
                    if (this.iso > isoMin)
                    {
                        this.iso = this.iso / 2;
                        snapCam.changeIso(this.iso);
                        change = true;
                    }
                }
                else if (!joyPress && change)
                {
                    change = false;
                }
                break;
            case 2:
                if (joyPress && !change)
                {
                    if ((this.verschlussZeit >= this.verschlusszeitWerte[this.verschlusszeitWerte.Count - 1]) && (this.verschlussZeit <= this.verschlusszeitWerte[0]))
                    {
                        Debug.Log("Verschlusszeit größer");
                        this.verschlussZeit = this.verschlusszeitWerte[this.verschlusszeitWerte.IndexOf(this.verschlussZeit) + 1];
                        snapCam.changeShutterSpeed(this.verschlussZeit);
                        change = true;
                    }
                }
                else if (!joyPress && change)
                {
                    change = false;
                }
                break;
            case 3:
                if (joyPress && !change)
                {
                    if ((this.verschlussZeit >= this.verschlusszeitWerte[this.verschlusszeitWerte.Count - 1]) && (this.verschlussZeit <= this.verschlusszeitWerte[0]))
                    {
                        Debug.Log("Verschlusszeit kleiner");
                        this.verschlussZeit = this.verschlusszeitWerte[this.verschlusszeitWerte.IndexOf(this.verschlussZeit) - 1];
                        snapCam.changeShutterSpeed(this.verschlussZeit);
                        change = true;
                    }
                }
                else if (!joyPress && change)
                {
                    change = false;
                }
                break;
            default:
                Debug.Log("nix");
                break;
        }
    }

    void OnPrimary2DAxisClick(bool b)
    {

        joyPress = b;
    }

    void OnSecondary2DAxisClick(bool b)
    {

        if (b)
        {
            Debug.Log("Nippel gedrückt");
        }
    }

    void OnTrigger(float f)
    {

        if (f >= 1)
        {
            //Debug.Log(f + "auslöser gedrück");
        }
    }


    void OnGripButton(bool b) {

        if (b == true && gripButton == false)
        {
            Debug.Log("Caro funktioniert");
            snapCam.CallTakeSnapshot();
            gripButton = true;
        }
        else if (b == false && gripButton == true)
        {
            gripButton = false;
        }
    }


    // Brennweite verändern
    void OnSecondary2DAxis(Vector2 b)
    {
        
        // wenn wir den Linken Joystick nach vorne drücken, dann erhöhen wir die Brennweite um 0,25
        if (b.y > 0.2)
        {
            Debug.Log("Dustin Brennweite geändert");
            brennweite += 0.25f;
            snapCam.changeFocalLength(brennweite);
            // wenn wir den linken Joystick nach hinten drücken, dann verringern wir die Brennweite um -0,25
        }
        else if (b.y < -0.2)
        {
            Debug.Log("Dustin Brennweite geändert");
            brennweite -= 0.25f;
            snapCam.changeFocalLength(brennweite);
        }
    }
}
