using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Obi;
using System.IO.Ports;

public class WeavingLineRenderer_TestWithArduino : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM8", 9600); //create a new port named 'stream'

    int buttonState = 0; //create a buttonState


    // Creates a line renderer that follows a Sin() function
    // and animates it.

    public Color c1 = Color.yellow;
    public Color c2 = Color.blue;
    //public int lengthOfLineRenderer = 60000;


    public float circleSpeed = 1f;
    public float circleSize = 1f;
    public float circleGrowSpeed = 1f;
    public float upDownHeight = 3f;
    float counter = 0;
    //public float nSticks = 7;

    //float[] xPosList = new float[6000];
    //float[] yPosList = new float[6000];
    //float[] zPosList = new float[6000];
    List<float> xPosList = new List<float>();
    List<float> yPosList = new List<float>();
    List<float> zPosList = new List<float>();

    private InputField input1;
    private InputField input2;
    private int origNumber;
    private int newBase = 10;
    private float floatNewBase;
    private int nFullCircles;
    private int nExtras;
    private int nLoopMarker;
    private int nFullCirclesNotInGrouping;
    private string lastDigit;
    private string secondLastDigit;
    private string thirdLastDigit;
    private string correctAnswer;
    public Button weaveButton;
    public Button showAnswerButton;
    public Button checkAnswerButton;
    public Text origNumberText;
    public Text newBaseText;

    public GameObject stickPrefab;
    //private int numberOfSticks = 7;
    private GameObject stickGlobal;


    void Awake()
    {
        input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
    }


    void Start()
    {
        stream.Open(); //open the stream, i.e. the port

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 4.5f;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        weaveButton.onClick.AddListener(WeaveOnClick); //****************************NEW LINE
        showAnswerButton.onClick.AddListener(ShowAnswerOnClick); //****************************NEW LINE
        checkAnswerButton.onClick.AddListener(CheckAnswerOnClick);


        //LineRenderer lineRenderer = GetComponent<LineRenderer>();

        //        for (int i = 0; i < lengthOfLineRenderer; i++)
        //        {
        //            var zPos = Mathf.Sin(counter * circleSpeed) * circleSize;
        //            var xPos = Mathf.Cos(counter * circleSpeed) * circleSize;
        //            var yPos = transform.position.y + Mathf.Cos(newBase / 2 * counter * circleSpeed) * upDownHeight;
        //
        //            circleSize += (circleGrowSpeed * Time.deltaTime);
        //            counter += Time.deltaTime;
        //              
        //            lineRenderer.SetPosition(i, new Vector3(xPos, yPos, zPos));
        //        }



    }


    void WeaveOnClick()
    {
        for (int i = 0; i < newBase; i++)

        {
            float[] angles;
            angles = new float[newBase];
            angles[i] = (i + 0) * 360 / newBase;

            //           Vector3 axis = new Vector3(i * 2.0f, 0, 0);
            GameObject stick = Instantiate(stickPrefab, transform.position, transform.rotation);
            //stick.transform.Rotate(0, (angles[i]+(360/newBase/2)), 0, Space.World);
            stick.transform.Rotate(0, (90 + angles[i] + (360 / newBase)), 0, Space.World);
            stickGlobal = stick;

        }

        nFullCircles = origNumber / newBase;
        nExtras = origNumber % newBase;
        nLoopMarker = nFullCircles / newBase;
        nFullCirclesNotInGrouping = nFullCircles - (nLoopMarker * newBase);
        thirdLastDigit = nLoopMarker.ToString();
        secondLastDigit = nFullCirclesNotInGrouping.ToString();
        lastDigit = nExtras.ToString();
        correctAnswer = thirdLastDigit + secondLastDigit + lastDigit;

    }

    void CheckAnswerOnClick()
    {
        float segmentMin = 2 * Mathf.PI * origNumber / (newBase * circleSpeed) - 1;
        float segmentMax = 2 * Mathf.PI * origNumber / (newBase * circleSpeed);
        if (counter > segmentMin && counter < segmentMax)
        {
            Debug.Log("CORRECT!!!");
        }
        else
        {
            Debug.Log("INCORRECT :(");
        }
    }

    void ShowAnswerOnClick()
    {
        nFullCircles = origNumber / newBase;
        nExtras = origNumber % newBase;
        nLoopMarker = nFullCircles / newBase;
        nFullCirclesNotInGrouping = nFullCircles - (nLoopMarker * newBase);
        thirdLastDigit = nLoopMarker.ToString();
        secondLastDigit = nFullCirclesNotInGrouping.ToString();
        lastDigit = nExtras.ToString();
        correctAnswer = thirdLastDigit + secondLastDigit + lastDigit;
        Debug.Log("Correct Answer is " + correctAnswer);

    }

    public void GetInput1(string inputNumber)
    {
        Debug.Log("Original number is " + inputNumber);
        origNumber = System.Int32.Parse(inputNumber);
        input1.text = "";
        origNumberText.text = inputNumber;
    }

    public void GetInput2(string inputBase)
    {
        Debug.Log("New base is " + inputBase);
        newBase = System.Int32.Parse(inputBase);
        input2.text = "";
        newBaseText.text = inputBase;
    }


    void Update()
    {
        string value = stream.ReadLine(); //read information coming through the stream i.e. the port
        buttonState = int.Parse(value); //convert the incoming value to integer and assign this to buttonState

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Transform lineRendererTransform = GetComponent<Transform>();

        var zPos = Mathf.Sin(counter * circleSpeed) * circleSize;
        var xPos = Mathf.Cos(counter * circleSpeed) * circleSize;
        //var origYPos = lineRendererTransform.position.y;
        floatNewBase = (float)newBase;
        var yPos = 6 + Mathf.Cos(floatNewBase / 2 * counter * circleSpeed) * upDownHeight;


        //if (Input.GetKey("right"))
        if (buttonState == 0)
        {
            //print("right key was pressed");
            print("0 = go right!");

            xPosList.Add(xPos);
            yPosList.Add(yPos);
            zPosList.Add(zPos);

            circleSize += (circleGrowSpeed * Time.deltaTime);
            counter += Time.deltaTime;

            print(xPosList.Count);

            lineRenderer.positionCount = xPosList.Count;
            
            for (int i = 0; i < xPosList.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(xPosList[i], yPosList[i], zPosList[i]));
            }
        }
        //else if (Input.GetKey("left"))
        else if (buttonState == 1)
        {
            //print("left key was pressed");
            print("1 = go left!");

            int counter2 = 1;

            xPosList.RemoveAt(xPosList.Count - counter2);
            yPosList.RemoveAt(yPosList.Count - counter2);
            zPosList.RemoveAt(zPosList.Count - counter2);

            circleSize -= (circleGrowSpeed * Time.deltaTime);
            counter -= Time.deltaTime;

            xPos = xPosList[xPosList.Count - 1];
            yPos = yPosList[yPosList.Count - 1];
            zPos = yPosList[zPosList.Count - 1];

            print(xPosList.Count);

            lineRenderer.positionCount = xPosList.Count;

            for (int i = 0; i < xPosList.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(xPosList[i], yPosList[i], zPosList[i]));
            }

        }
    }

}


