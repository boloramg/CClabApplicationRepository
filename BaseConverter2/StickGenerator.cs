using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class StickGenerator : MonoBehaviour
{
    public GameObject stickPrefab;
    public int numberOfSticks = 7;
    private GameObject stickGlobal;    

    // Start is called before the first frame update

    void Start ()
    {
        for (int i = 0; i < numberOfSticks; i++)
            
        {
            float[] angles;
            angles = new float[numberOfSticks];
            angles[i] = (i + 0) * 360 / numberOfSticks;
 
 //           Vector3 axis = new Vector3(i * 2.0f, 0, 0);
            GameObject stick = Instantiate(stickPrefab, transform.position, transform.rotation);
            //stick.transform.Rotate(0, (angles[i]+(360/numberOfSticks/2)), 0, Space.World);
            stick.transform.Rotate(0, (90+angles[i] + (360 / numberOfSticks)), 0, Space.World);
            stickGlobal = stick;

        }
}

    // Update is called once per frame
    void Update()
    {
        //BoxCollider bc = stickGlobal.AddComponent<BoxCollider>() as BoxCollider;
        //ObiCollider oc = stickGlobal.AddComponent<ObiCollider>() as ObiCollider;
        //oc.Phase = 2;
    }
}
