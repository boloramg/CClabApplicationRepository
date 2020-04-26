using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class PlayheadMover : MonoBehaviour
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


    //Playhead related variables
    RectTransform playheadRectTransform;
    RectTransform newTwistRectTransform;
    float x;
    Vector2 pos;
    float speed;
    public int scaler;
    public Transform twistPrefab;
    public Transform guide1;
    public Transform guide2;
    public Transform guide3;
    public Transform guide4;
    public Transform guide5;
    Transform newTwist;
    Vector3 scaleOfPos;

    static public int score;
    bool twisted;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        twisted = false;

        playheadRectTransform = GetComponent<RectTransform>();
        scaleOfPos = new Vector3 (1, 1, 1);

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

        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        pos = playheadRectTransform.anchoredPosition;
        //Debug.Log(pos.x);
        speed = Time.deltaTime * 50;
        pos.x += speed;
        playheadRectTransform.anchoredPosition = pos;


        if (serialInput != null) {
            string[] strEul = serialInput.Split(';');  // parses using semicolon ; into a string array called strEul.
            //Debug.Log(strEul[1]);
            if (strEul.Length == 3) {
                if (int.Parse(strEul[1]) == 1 && pos.x>-550 && pos.x<550){
                    newTwist = Instantiate(twistPrefab, pos, Quaternion.identity);
                    newTwist.transform.parent = gameObject.transform.parent;
                    newTwist.transform.localScale = scaleOfPos;
                    newTwistRectTransform = newTwist.GetComponent<RectTransform>();
                    newTwistRectTransform.anchoredPosition = pos;
                    ScoreKeeper(guide1);
                    ScoreKeeper(guide2);
                    ScoreKeeper(guide3);
                    ScoreKeeper(guide4);
                    ScoreKeeper(guide5);
                    Debug.Log("score" + score);
                    twisted = true;
                }
                else { }
            }
        }

        if (pos.x > 750) {
            SceneManager.LoadScene(sceneIndex);
        }

    }

    void ScoreKeeper(Transform guide) {
        float x = guide.position.x;
        float xminA = x-8.0f;
        float xmaxA = x+8.0f;
        float xminB = x-20.0f;
        float xmaxB = x+20.0f;
        if (xminA < pos.x || pos.x < xmaxA) {
            score = score + 3;
        }
        else if (xminB < pos.x && pos.x < xminA || pos.x < xmaxB && pos.x > xmaxA)
        {
            score = score + 1;
        }
        else if (pos.x < xminB || pos.x > xmaxB) {
            score = score + 0;
        }
    }

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


    public IEnumerator Wait()
    {
        //yield return new WaitForSeconds(2.0f);
        while (twisted) { 
        yield return new WaitForSeconds(2.5f);
        twisted = false;
        }
    }
}
