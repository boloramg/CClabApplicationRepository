using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;
using System;

public class SceneChange : MonoBehaviour
{
    public int sceneIndex;

    //Serial Port related variables
    private SerialPort serialPort = null;
    private String portName = "COM8";  // use the port name for your Arduino, such as /dev/tty.usbmodem1411 for Mac or COM3 for PC 
    private int baudRate = 9600;  // match your rate from your serial in Arduino
    private int readTimeOut = 100;

    private string serialInput;

    bool programActive = true;
    Thread thread;

    // Start is called before the first frame update
    void Start()
    {
        //Serial stuff
        try
        {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.ReadTimeout = readTimeOut;
            serialPort.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        thread = new Thread(new ThreadStart(ProcessData));  // serial events are now handled in a separate thread
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (serialInput != null)
        {
            string[] strEul = serialInput.Split(';');  // parses using semicolon ; into a string array called strEul.
            Debug.Log(strEul[2]);
            if (strEul.Length == 3)
            {
                if (int.Parse(strEul[2]) == 1)
                {
                    SceneManager.LoadScene(sceneIndex);
                }
                else { }
            }
        }


        //if (Input.GetMouseButton(0)) {
        //    SceneManager.LoadScene(sceneIndex);
        //}
    }

    //public void SceneLoader(int sceneIndex)
    //{
    //    SceneManager.LoadScene(sceneIndex);
    //}

    void ProcessData()
    {
        Debug.Log("Thread: Start");
        while (programActive)
        {
            try
            {
                serialInput = serialPort.ReadLine();
            }
            catch (TimeoutException)
            {

            }
        }
        Debug.Log("Thread: Stop");
    }

    //Serial related
    public void OnDisable()  // attempts to closes serial port when the gameobject script is on goes away
    {
        programActive = false;
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
