using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private InputField input1;
    private InputField input2;
    private int origNumber;
    private int newBase = 10;
    private int nFullCircles;
    private int nExtras;
    private int nLoopMarker;
    private int nFullCirclesNotInGrouping;
    private string lastDigit;
    private string secondLastDigit;
    private string thirdLastDigit;
    private string correctAnswer;
    public Button calculateButton;
    public Text origNumberText;
    public Text newBaseText;


    void Awake () {
        input1 = GameObject.Find("InputField1").GetComponent<InputField>();
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
    }

    void Start() {
        calculateButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() {
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

    public void GetInput1(string inputNumber) {
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

//    void Update() {
//        nFullCircles = origNumber / newBase;
 //       nExtras = origNumber % newBase;
 //       nLoopMarker = nFullCircles / newBase;
 //       nFullCirclesNotInGrouping = nFullCircles - (nLoopMarker * newBase);
  //      thirdLastDigit = nLoopMarker.ToString();
  //      secondLastDigit = nFullCirclesNotInGrouping.ToString();
//        lastDigit = nExtras.ToString();
//        correctAnswer = thirdLastDigit + secondLastDigit + lastDigit;
//        Debug.Log(correctAnswer);
//
//    }

}
