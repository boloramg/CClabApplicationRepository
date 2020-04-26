using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitmapDrawing;


public class NewBehaviourScript : MonoBehaviour
{
    public Color c1;

    [Range(1f, 20f)]
    public float radius = 1f;

    [Range(1, 1000)]
    public int numberofparticles = 100;

    public int kCusps = 10; // a point on a curve where a moving point on the curve must start to move backward.

    private Vector2[] pointsArray;
    Vector2 p;
    Vector2 q;
    public int size;
    Texture texture;

    void Start()
    {
        p = new Vector2(0f, 0f); // p is Vector3.zero to start

        Material material = GetComponent<Renderer>().material;
        Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point; //filterMode is what makes the pixels not blurry
        material.SetTexture("_MainTex", texture);

        for (int i = 0; i < numberofparticles; i++)
        {
            p = PointOnEpicycloid(radius, 2 * Mathf.PI * (i / (float)(numberofparticles - 1)), kCusps); //for every i, p is PointOnEpicycloid
            //p.z = i; //but p.z will be the i
            q = PointOnEpicycloid(radius, 2 * Mathf.PI * (i+1 / (float)(numberofparticles - 1)), kCusps);

            texture.DrawLine((int)p.x, (int)p.y, (int)q.x, (int)q.y, Color.red);
            texture.Apply();
        }

    }

    // Update is called once per frame
    void Update()
    {

        Material material = GetComponent<Renderer>().material;
        Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point; //filterMode is what makes the pixels not blurry
        material.SetTexture("_MainTex", texture);

        for (int i = 0; i < numberofparticles; i++)
        {
            p = PointOnEpicycloid(radius, Mathf.Repeat(Time.time, 5)+2 * Mathf.PI * (i / (float)(numberofparticles - 1)), kCusps); //for every i, p is PointOnEpicycloid
            //p.z = i; //but p.z will be the i
            Debug.Log(Time.time);
            q = PointOnEpicycloid(radius, Mathf.Lerp(Time.time, 0, 0.5f) + 2 * Mathf.PI * (i + 1/ (float)(numberofparticles - 1)), kCusps);

            //c1 = new Color(1.0f, 0.0f, 0.0f);
            float h, s, v;
            Color.RGBToHSV(c1, out h, out s, out v); //this function outputs 3 values, so we declare these out values as arguments

            h = Mathf.Repeat(h + Mathf.Repeat((float)i / numberofparticles*2, 0.2f), 1f);
            Color newColor = Color.HSVToRGB(h, s, v);

            texture.DrawLine((int)(p.x), (int)(p.y), (int)q.x, (int)q.y, newColor);
            texture.Apply();
        }
        //FunctionX(numberofparticles, radius, kCusps);

    }

    public Vector2 PointOnEpicycloid(float radius, float angle, int kCusps)
    {
        //float angleInRadians = angle * Mathf.Deg2Rad;

        //below I've used the parametric equation for an epicycloid
        return new Vector2(
            x: (radius * (kCusps + 1) * Mathf.Cos(angle) - radius * Mathf.Cos((kCusps + 1) * angle))*12+(size/2),
            y: (radius * (kCusps + 1) * Mathf.Sin(angle) - radius * Mathf.Sin((kCusps + 1) * angle))*12+(size/2)
            );
    }

}
