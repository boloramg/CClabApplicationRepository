using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleDrawer : MonoBehaviour
{
    public float ThetaScale = 0.02f;
    public float radius = 0.1f;
    private int particleCount;
    private LineRenderer LineDrawer;
    private float Theta = 0f;

    void Start()
    {
        DrawCircle(radius, Theta, ThetaScale); //this script is not used in the final version of the prorject
    }

    void Update()
    {

    }


    public void DrawCircle(float radius, float Theta, float ThetaScale)
    {
        LineDrawer = GetComponent<LineRenderer>();
        particleCount = (int)((1f / ThetaScale) + 1f);
        LineDrawer.positionCount = particleCount;
        for (int i = 0; i < particleCount; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float z = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, 0, z));
            LineDrawer.widthMultiplier = 0.1f;
        }
    }
}
