using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMove : MonoBehaviour
{
    //public float speed = 10.0f;
    //public float rotationSpeed = 100.0f;
    public float circleSpeed = 1f;
    public float forwardSpeed = -1.0f; // Assuming negative Z is towards the camera (NOT USED)
    public float circleSize = 0.1f;
    public float circleGrowSpeed = 0.001f;
    public float upDownHeight = 0f;
    float counter = 0;
    public float nSticks = 6;
    public float v;

    // Start is called before the first frame update
    void Start()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    }

    // Update is called once per frame
    void Update()
    {
        var zPos = Mathf.Sin(counter * circleSpeed) * circleSize;
        var xPos = Mathf.Cos(counter * circleSpeed) * circleSize;
        //var zPos = Mathf.Sin(counter * (v / circleSize)) * circleSize;
        //var xPos = Mathf.Cos(counter * (v / circleSize)) * circleSize;
        
        //var yPos = Mathf.Sin(5 * counter * circleSpeed) * upDownHeight;
        var yPos = transform.position.y + Mathf.Cos(nSticks / 2 * counter * circleSpeed) * upDownHeight;
        //var zPos = forwardSpeed * Time.deltaTime;
        //zPos += forwardSpeed * Time.deltaTime;

        circleSize += (circleGrowSpeed*Time.deltaTime);
        counter += Time.deltaTime;

        Vector3 Position =new Vector3 (xPos, yPos, zPos);
        transform.position = Position;

    }
}
