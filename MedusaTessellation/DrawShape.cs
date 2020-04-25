using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    LineRenderer myLine;
    
    // Start is called before the first frame update
    void Start()
    {
        //myLine = GetComponent<LineRenderer>();
        //MakeShape(5,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeShape(int sides, float shapeSize, float noiseAmt) {
        myLine = GetComponent<LineRenderer>();

        List<Vector3> verts = new List<Vector3>();
        for (int i = 0; i < sides; i++)
        {
            Vector3 displacedPosition = this.transform.position;
            displacedPosition += PointOnCircle(noiseAmt, Random.Range(0,Mathf.PI*2f));


            verts.Add(displacedPosition + PointOnCircle(shapeSize, i * (Mathf.PI * 2f / (float)sides))); //(float)sides = this is casting 'sides' to float so that we can do float based division
        }
        verts.Add(verts[0]);

        myLine.positionCount = verts.Count;
        myLine.SetPositions(verts.ToArray());
    }

    Vector3 PointOnCircle (float radius, float theta)
    {
        Vector3 toreturn = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0);
        return toreturn;

    }

}
