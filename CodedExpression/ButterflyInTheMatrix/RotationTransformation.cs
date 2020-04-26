using UnityEngine;


public class RotationTransformation : Transformation
//RotationTransformation class extends on, or inherits from, the abstract class Transformation 
{
    public Vector3 rotation;

    public override Matrix4x4 Matrix { get
        //we're overriding the abstract method Apply from abstract Transformation class with a concrete Apply method
        {
            float radX = rotation.x * Mathf.Deg2Rad; //converting from degree to radian
            float radY = rotation.y * Mathf.Deg2Rad; //converting from degree to radian
            float radZ = rotation.z * Mathf.Deg2Rad; //converting from degree to radian
            float sinX = Mathf.Sin(radX); //sine of rotation along x
            float cosX = Mathf.Cos(radX); //cosine of rotation along x
            float sinY = Mathf.Sin(radY); //sine of rotation along y
            float cosY = Mathf.Cos(radY); //cosine of rotation along y

            //any (x,y) point when rotated around th z axis equals to x(cosZ, sinZ) + y(-sinZ, cosZ)
            //i.e. equals to the sum of x and y multiplied by (cosZ, sinZ) and (-sinZ, cosZ), respectively 
            //(cosZ,sinZ) is the x, y coordinates of a point on a unit circle as it rotates counter-clockwise starting at (1,0). (1,0) indicates direction of x axis
            //(-sinZ,cosZ) is the x, y coordinates of a point on a unit circle as it rotates counter-clockwise starting at (0,1). (0,1) indicates direction of y axis
            float sinZ = Mathf.Sin(radZ); //sine of rotation along z
            float cosZ = Mathf.Cos(radZ); //cosine of rotation along z


            //All x, y, z axes rotation unified matrix terms calculated below: x-axiz = 1st column; y-axis = 2nd column; z-axis = 3rd column
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetColumn(0, new Vector4(
                cosY * cosZ,
                cosX * sinZ + sinX * sinY * cosZ,
                sinX * sinZ - cosX * sinY * cosZ,
                0f
            ));
            matrix.SetColumn(1, new Vector4(
                -cosY * sinZ,
                cosX * cosZ - sinX * sinY * sinZ,
                sinX * cosZ + cosX * sinY * sinZ,
                0f
            ));
            matrix.SetColumn(2, new Vector4(
                sinY,
                -sinX * cosY,
                cosX * cosY,
                0f
            ));

            matrix.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
            return matrix;
        }

        //return xAxis * point.x + yAxis * point.y + zAxis * point.z; //essentially multiplying vector (x,y,z) by the unified rotation matrix determined above

        //return new Vector3(
        //    point.x * cosZ - point.y * sinZ, //x(cosZ, sinZ) + y(-sinZ, cosZ) = (xcosZ, xsinZ) + (-ysinZ, ycosZ) = (xcosZ-ysinZ, xsinZ+ycosZ) -> this is vector addition
        //    point.x * sinZ + point.y * cosZ,
        //    point.z
        //);
    }

    void Update()
    {
        rotation.x = 100f * Mathf.Cos(Time.time)*Mathf.Tan(5f);
        rotation.y = 50f * Mathf.Tan(Time.time) * Mathf.Sin(Time.time)/Mathf.Sin(5f);
    }

}