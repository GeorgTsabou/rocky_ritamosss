using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCTarget : MonoBehaviour
{
    public  OSC osc; 
    public string ValueHeader = "/Yorgos";
    public float[] Val = new float[7];


    // Start is called before the first frame update
    void Start()
    {
        osc = gameObject.GetComponent<OSC>();
        osc.SetAddressHandler(ValueHeader, OnValue);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            osc.SetAddressHandler(ValueHeader, OnValue);
        }
        */
    }

    void OnValue(OscMessage message)
    {
        Debug.Log("message:" + message);

        float x = message.GetFloat(0);
        Debug.Log("X:" + x);
        float y = message.GetFloat(1);
        Debug.Log("Y:" + y);
        float z = message.GetFloat(2);
        Debug.Log("Z:" + z);

        transform.position = new Vector3(x, y, z);

    }
}
