using UnityEngine;

public class CameraTransformation : Transformation
{
    public float focalLength = 6f; //a larger focal length means we're zooming in

    public override Matrix4x4 Matrix
    
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(focalLength, 0f, 0f, 0f)); //scaling x by focal length
            matrix.SetRow(1, new Vector4(0f, focalLength, 0f, 0f)); //scaling y by focal length
            matrix.SetRow(2, new Vector4(0f, 0f, 0f, 0f)); 
            //Making all z values 0, makes the grid go from 3D to 2D, thus creating a rudimentary orthographic camera projection.
            matrix.SetRow(3, new Vector4(0f, 0f, 1f, 0f));
            //0,0,1,0 as the bottom row makes the fourth coordinate of the result equal to the original z coordinate.
            //Then we convert from homogeneous to euclidean coordinates by dividing everything by z.
            //Play with z in PositionTransformation to bring all points in front of the camera
            //...as perspective projection moves all points towards the camera's position – the origin – until they hit the plane. 
            return matrix;
        }
    }
}