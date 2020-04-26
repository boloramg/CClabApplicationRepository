using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePinConstraint : MonoBehaviour
{
    public float rotationAngle = 0;
    float radius;
    public float speed = 0.1f;
    Vector3 origPosition;
    Vector3 newPosition;
    float theta;
    Renderer rend;
    

    // Start is called before the first frame update
    void Start()
    {
        origPosition = this.transform.position;
        radius = (float)origPosition.y;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        theta = Mathf.Abs(Mathf.PI * Mathf.Sin(speed*Time.time));
        Debug.Log(theta);

        newPosition = origPosition;
        newPosition.x = radius * Mathf.Cos(theta);
        newPosition.y = radius * Mathf.Sin(theta);
        this.transform.position = newPosition;
    }

}
