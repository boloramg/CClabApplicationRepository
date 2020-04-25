using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeBehaviour : MonoBehaviour
{

    ObiRopeCursor cursor; //cursor component on the rope - a bit like how we place a cursor inside a Word document
    ObiRope rope;
    public float minLength = 0.1f;
    public GameObject shapePrefab;

    // Use this for initialization
    void Start()
    {
        cursor = GetComponentInChildren<ObiRopeCursor>(); //gets the cursor component on the rope object
        rope = cursor.GetComponent<ObiRope>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (rope.RestLength > minLength)
                cursor.ChangeLength(rope.RestLength - 1f * Time.deltaTime); //reduces rope length
        }

        if (Input.GetKey(KeyCode.S))
        {
            cursor.ChangeLength(rope.RestLength + 1f * Time.deltaTime); //increases rope length
        }

        if (Input.GetKey(KeyCode.A))
        {
            rope.transform.Translate(Vector3.left * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rope.transform.Translate(Vector3.right * Time.deltaTime, Space.World);
        }

    }

    //void OnMouseOver()
    //{
    //    //If your mouse hovers over the GameObject with the script attached, output this message
    //    Debug.Log("Mouse is over GameObject.");
    //}

    //void OnMouseExit()
    //{
    //    //The mouse is no longer hovering over the GameObject so output this message each frame
    //    Debug.Log("Mouse is no longer on GameObject.");
    //}

}
