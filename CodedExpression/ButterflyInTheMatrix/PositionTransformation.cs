using UnityEngine;

public class PositionTransformation : Transformation
    //PositionTransformation class extends on, or inherits from, the abstract class Transformation 
{

    public Vector3 position;

    //public override Vector3 Apply(Vector3 point) 
    //    //we're overriding the abstract method Apply from abstract Transformation class with a concrete Apply method
    //{
    //    return point + position;
    //}

    //the Apply method needs to be changed to Matrix properties
        public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(1f, 0f, 0f, position.x));
            matrix.SetRow(1, new Vector4(0f, 1f, 0f, position.y));
            matrix.SetRow(2, new Vector4(0f, 0f, 1f, 5f)); //offsetting by 5 along z axis
            matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }
    }

}