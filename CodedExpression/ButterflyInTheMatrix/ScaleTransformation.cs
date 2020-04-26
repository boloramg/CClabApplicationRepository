using UnityEngine;

public class ScaleTransformation : Transformation
//ScaleTransformation class extends on, or inherits from, the abstract class Transformation 
{

    public Vector3 scale;

    //public override Vector3 Apply(Vector3 point)
    ////we're overriding the abstract method Apply from abstract Transformation class with a concrete Apply method
    //{
    //    point.x *= scale.x; //multiplies the original point's position by the scale vector
    //    point.y *= scale.y;
    //    point.z *= scale.z;
    //    return point;
    //}

    //the Apply method needs to be changed to Matrix properties
    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(scale.x, 0f, 0f, scale.y));
            matrix.SetRow(1, new Vector4(0f, scale.y, 0f, 0f));
            matrix.SetRow(2, new Vector4(scale.x, 0f, scale.z, 0f));
            matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }

    void Update() {
        scale.x = 100f * Mathf.Sin(Time.time);
        scale.y = 100f * Mathf.Cos(Time.time);
        scale.z = 50f * Mathf.Sin(Time.time);

    }
}