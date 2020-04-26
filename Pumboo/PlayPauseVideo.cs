using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Threading;
using System;

public class PlayPauseVideo : MonoBehaviour
{
    public int sceneIndex;

    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public VideoPlayer videoPlayer4;
    public VideoPlayer videoPlayer5;
    public VideoPlayer videoPlayer6;
    public VideoPlayer videoPlayer7;
    public VideoPlayer videoBackground;
    public VideoPlayer videoZeroPump;
    private int counter;
    //private int pumpVal;
    //private int pumpVal2;
    private int outcome;
    public GameObject keepPumpingText;
    public GameObject wellDoneText;
    public GameObject gameOverText;

    private SerialPort serialPort = null;
    private String portName = "COM8";  // use the port name for your Arduino, such as /dev/tty.usbmodem1411 for Mac or COM3 for PC 
    private int baudRate = 9600;  // match your rate from your serial in Arduino
    private int readTimeOut = 100;

    private string serialInput;

    bool programActive = true;
    Thread thread;

    public AudioClip soundTriggered1;
    public AudioSource audioSource1;


    void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        soundTriggered1 = audioSource1.clip;
        counter = 0;
        outcome = 0;
        keepPumpingText.SetActive(false);
        wellDoneText.SetActive(false);
        gameOverText.SetActive(false);
        

        StartCoroutine(Wait());
        StartCoroutine(KeepPumpingWait1());
        StartCoroutine(KeepPumpingWait2());
        StartCoroutine(KeepPumpingWait3());
        StartCoroutine(GameOverWait());

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
        Debug.Log(counter);
        if (serialInput != null)
        {
            string[] strEul = serialInput.Split(';');  // parses using semicolon ; into a string array called strEul.
            //Debug.Log(strEul[1]);
            if (strEul.Length == 3)
            {
                if (int.Parse(strEul[0]) == 1)
                {
                    videoZeroPump.Pause();
                    counter = counter + 1;//
                    //pumpVal = 1;
                }
                else if (int.Parse(strEul[0]) == 0) {
                    //pumpVal = 0;
                }

                if (int.Parse(strEul[2]) == 1)
                {
                    if (counter == 8 || counter == 10 || counter == 12)
                    {
                        //wellDoneText.SetActive(true);
                        outcome = 1;
                    }
                    else if (counter == 2)
                    {
                        //keepPumpingText.SetActive(true);
                        outcome = 2;
                    }
                    else if (counter == 4)
                    {
                        //keepPumpingText.SetActive(true);
                        outcome = 3;
                    }
                    else if (counter == 6)
                    {
                        //keepPumpingText.SetActive(true);
                        outcome = 4;
                    }
                    else if (counter == 14)
                    {
                        //gameOverText.SetActive(true);
                        outcome = 5;
                    }
                }
                else { }
            }
            serialInput = null;
        }


        if (counter == 14)
        {
            //gameOverText.SetActive(true);
            outcome = 5;
        }

        //if (Input.GetKeyDown("space"))
        //{
        //    videoZeroPump.Pause();
        //    counter = counter + 1;
        //    Debug.Log(counter);
        //}


        if (counter == 1)
        {
            videoPlayer1.Play();
            counter = 2;
        }

        if (counter == 3 && videoPlayer1.isPaused)
        {
            videoPlayer2.Play();
            counter = 4;
        }

        if (counter == 5 && videoPlayer2.isPaused)
        {
            videoPlayer3.Play();
            counter = 6;
        }

        if (counter == 7 && videoPlayer3.isPaused)
        {
            videoPlayer4.Play();
            counter = 8;
        }

        if (counter == 9 && videoPlayer4.isPaused)
        {
            videoPlayer5.Play();
            counter = 10;
        }

        if (counter == 11 && videoPlayer5.isPaused)
        {
            videoPlayer6.Play();
            counter = 12;
        }

        if (counter == 13 && videoPlayer6.isPaused)
        {
            videoPlayer7.Play();
            counter = 14;
        }

        //if (serialInput != null)
        //{
        //    string[] strEul = serialInput.Split(';');  // parses using semicolon ; into a string array called strEul.
        //    Debug.Log(strEul[1]);
        //    if (strEul.Length == 3)
        //    {
        //        if (int.Parse(strEul[2]) == 1)
        //        {
        //            if (counter == 8 || counter == 10 || counter == 12)
        //            {
        //                //wellDoneText.SetActive(true);
        //                counter = 15;
        //            }
        //            else if (counter == 4 || counter == 6)
        //            {
        //                keepPumpingText.SetActive(true);
        //                counter = 16;
        //            }
        //            else if (counter == 14)
        //            {
        //                gameOverText.SetActive(true);
        //                counter = 17;
        //            }
        //        }
        //        else { }
        //    }
        //}


        //if (Input.GetMouseButton(0))
        //{
        //    if (counter == 8 || counter == 10 || counter == 12)
        //    {
        //        //wellDoneText.SetActive(true);
        //        outcome = 1;
        //    }
        //    else if (counter == 4 || counter == 6)
        //    {
        //        keepPumpingText.SetActive(true);
        //        outcome = 2;
        //    }
        //    else if (counter == 14)
        //    {
        //        gameOverText.SetActive(true);
        //        outcome = 3;
        //    }
        //}

    }

    IEnumerator Wait() {
            //yield return new WaitForSeconds(2.0f);
            yield return new WaitUntil(() => outcome == 1);
            //videoBackground.Play();
            wellDoneText.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator KeepPumpingWait1()
    {
        yield return new WaitUntil(() => outcome == 2);
        keepPumpingText.SetActive(true);
        if (soundTriggered1 != null)
        {
            audioSource1.PlayOneShot(soundTriggered1, 0.7F);
        }
        yield return new WaitForSeconds(2.5f);
        keepPumpingText.SetActive(false);
        //SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator KeepPumpingWait2()
    {
        yield return new WaitUntil(() => outcome == 3);
        keepPumpingText.SetActive(true);
        if (soundTriggered1 != null)
        {
            audioSource1.PlayOneShot(soundTriggered1, 0.7F);
        }
        yield return new WaitForSeconds(2.5f);
        keepPumpingText.SetActive(false);
        //SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator KeepPumpingWait3()
    {
        yield return new WaitUntil(() => outcome == 4);
        keepPumpingText.SetActive(true);
        if (soundTriggered1 != null)
        {
            audioSource1.PlayOneShot(soundTriggered1, 0.7F);
        }
        yield return new WaitForSeconds(2.5f);
        keepPumpingText.SetActive(false);
        //SceneManager.LoadScene(sceneIndex);
    }


    IEnumerator GameOverWait()
    {
        yield return new WaitUntil(() => outcome == 5);
        //videoPlayer7.Play();
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene(0);
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
}
