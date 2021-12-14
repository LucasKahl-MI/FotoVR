using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class inputReader : MonoBehaviour
{

    public InputDeviceCharacteristics character;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update-Methode aufgerufen");
        List<InputDevice> devices = new List<InputDevice>();
        //Debug.Log("DeviceListe erstellt");
        InputDevices.GetDevicesWithCharacteristics(character, devices);

        if (devices.Count == 0)
        {
            return;
        }

        InputDevice dev = devices[0];
        //Debug.Log("Devices Array Größe "+ devices.Count);

        bool b = false;
        float fl;
        Vector2 v = new Vector2();

        InputFeatureUsage<bool>[] btms = new InputFeatureUsage<bool>[] {
            CommonUsages.triggerButton,
            CommonUsages.menuButton,
            CommonUsages.primary2DAxisClick,
            CommonUsages.secondary2DAxisClick,
            CommonUsages.gripButton
        };

        //Debug.Log("FeatureUsage Booleans verarbeitet");

        InputFeatureUsage<float>[] ftms = new InputFeatureUsage<float>[] {
            CommonUsages.trigger
        };

        //Debug.Log("FeatureUsage Floats verarbeitet");

        InputFeatureUsage<Vector2>[] axis = new InputFeatureUsage<Vector2>[] {
            CommonUsages.primary2DAxis,
            CommonUsages.secondary2DAxis
        };


        foreach (InputFeatureUsage<bool> f in btms)
        {
            if (dev.TryGetFeatureValue(f, out b))
            {
                SendMessage($"On{f.name}", b, SendMessageOptions.DontRequireReceiver);
            }
        }

        foreach (InputFeatureUsage<float> f in ftms)
        {
            if (dev.TryGetFeatureValue(f, out fl))
            {
                SendMessage($"On{f.name}", fl, SendMessageOptions.DontRequireReceiver);
            }
        }

        foreach (InputFeatureUsage<Vector2> f in axis)
        {
            if (dev.TryGetFeatureValue(f, out v))
            {
                SendMessage($"On{f.name}", v, SendMessageOptions.DontRequireReceiver);
            }
        }

        //dev.TryGetFeatureValue(CommonUsages.)
        //Debug.Log("Position Controller: " + CommonUsages.devicePosition);

        /*GameObject g = GameObject.FindGameObjectWithTag("rightController");
        transform.position = g.transform.position;
        transform.rotation = g.transform.rotation;*/
    }
}

