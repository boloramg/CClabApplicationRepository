using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeLengthController : MonoBehaviour
{
    public float speed = 10;
    ObiRopeCursor cursor;
    ObiRope rope;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GetComponentInChildren<ObiRopeCursor>();
        rope = cursor.GetComponent<ObiRope>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.S))
        //{
        //    cursor.ChangeLength(rope.RestLength + speed * Time.deltaTime);
        //}

        cursor.ChangeLength(rope.RestLength + speed * Time.deltaTime);
    }
}
