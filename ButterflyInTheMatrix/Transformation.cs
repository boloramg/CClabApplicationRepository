using UnityEngine;

public abstract class Transformation : MonoBehaviour 
    //an abstract class cannot be used directly
{
    public abstract Matrix4x4 Matrix { get; }
    //an abstract read-only property is added to retrieve the transformation matrix

    public Vector3 Apply(Vector3 point) //Apply method no longer needs to be abstract. It will just grab the matrix and perform the multiplication.
    //the transformation components (which are concrete transformation components) will inherit this abstract Apply method to do their job
    {
		return Matrix.MultiplyPoint(point);
	}


}


