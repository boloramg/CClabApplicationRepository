using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2, point3;

    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = numPoints;
        //DrawLinearCurve();
        //DrawQuadraticCurve();
        DrawCubicCurve();
    }

    // Update is called once per frame
    void Update()
    {
        DrawCubicCurve();
    }

    //private void DrawLinearCurve()
    //{
    //    for (int i = 1; i<50+1; i++)
    //    {
    //        float t = i / (float)numPoints;
    //        positions[i - 1] = CalculateLinearBezierPoint(t, point0.position, point1.position);
    //    }
    //    lineRenderer.SetPositions(positions);
    //}

    //private void DrawQuadraticCurve()
    //{
    //    for (int i = 1; i < 50 + 1; i++)
    //    {
    //        float t = i / (float)numPoints;
    //        positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
    //    }
    //    lineRenderer.SetPositions(positions);
    //}

    private void DrawCubicCurve()
    {
        for (int i = 1; i < 50 + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // return = (1-t)2 P0 + 2(1-t)tP1 + t2P2
        //            u            u       tt
        //           uu * P0  + 2 * u * t * P1 + tt * P2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //return = (1 - t)3 P0 + 3(1-t)2 t P1 + 3(1-t) t2 P2 + t3 P3 
        //          uuu * p0  +  3 * uu * t * p1 + 3*u*tt*p2 + ttt * p3
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;
        return p;
    }
}
