using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButtonScript : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM8", 9600); //create a new port named 'stream'

    int buttonState = 0; //create a buttonState
       
    // Start is called before the first frame update
    void Start()
    {
        stream.Open(); //open the stream, i.e. the port
    }

    // Update is called once per frame
    void Update()
    {
        string value = stream.ReadLine(); //read information coming through the stream i.e. the port
        buttonState = int.Parse(value); //convert the incoming value to integer and assign this to buttonState
        GetComponent<Renderer>().material.color = new Color(buttonState / 1023.0f, 0, 0);
        //if (buttonState > 0)
        //{
        //    GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        //}
        //else if (buttonState == 0)
        //{
        //    GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        //}

    }
}
