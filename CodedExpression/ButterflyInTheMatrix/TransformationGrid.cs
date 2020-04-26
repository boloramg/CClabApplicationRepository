using UnityEngine;
using System.Collections.Generic;

public class TransformationGrid : MonoBehaviour {

	public Transform prefab; //grab the Transform component of a public prefab

	public int gridResolution = 10; //set the resolution of our grid to 10 (modifiable public integer)

	Transform[] grid; //declare an array of transform components called grid

    List<Transformation> transformations; //declare a list of all components that use the abstract Transformation class 
                                          //call this list transformations

    Matrix4x4 transformation; //combined matrix of all transformation matrices

    void Awake () {
		grid = new Transform[gridResolution * gridResolution * gridResolution]; 
        //the grid array contains all transforms of points forming the cube matrix
        
		for (int i = 0, z = 0; z < gridResolution; z++) {
			for (int y = 0; y < gridResolution; y++) {
				for (int x = 0; x < gridResolution; x++, i++) {
					grid[i] = CreateGridPoint(x, y, z); 
                    //creates grid points with x, y, z coordinates from 0 to 9 each; supplies x, y, z to the CreateGridPoint function
				}
			}
		}
        transformations = new List<Transformation>(); //create the transformations list
    }

    void Update()
    {
        UpdateTransformation();
        GetComponents<Transformation>(transformations); //get all components of type Transformation in transformations list 
        for (int i = 0, z = 0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                for (int x = 0; x < gridResolution; x++, i++)
                {
                    grid[i].localPosition = TransformPoint(x, y, z); //transform the x, y, z of all points
                }
            }
        }
    }

    Transform CreateGridPoint (int x, int y, int z) {
		Transform point = Instantiate<Transform>(prefab); //each point is an instantiation of the prefab
		point.localPosition = GetCoordinates(x, y, z); //set as the point's localPosition the output from GetCoordinates()
		point.GetComponent<MeshRenderer>().material.color = new Color(
            //each point is colored in relation to its position
			(float)0.2f,
			(float)y / gridResolution,
			(float)0.6f
		);
		return point;
	}


    //GetCoordinates makes sure the cube grid's points are such that the cube grid's center is at 0,0,0.
    //i.e. x = -4.5; -3.5; -2.5; -1.5; -0.5; 0.5, 1.5; 2.5; 3.5; 4.5
	Vector3 GetCoordinates (int x, int y, int z) { 
		return new Vector3(
			x - (gridResolution - 1) * 0.5f,
			y - (gridResolution - 1) * 0.5f,
			z - (gridResolution - 1) * 0.5f
		);
	}

    Vector3 TransformPoint(int x, int y, int z)
    {
        Vector3 coordinates = GetCoordinates(x, y, z); //get the original coordinates
        //for (int i = 0; i < transformations.Count; i++)
        //{
        //    coordinates = transformations[i].Apply(coordinates); //apply all transformations inside transformations list to original coordinates
        //}
        //return coordinates;
        return transformation.MultiplyPoint(coordinates); 
        //instead of invoking Apply, return transformation - which performs matrix multiplication - instead
    }

    void UpdateTransformation()
    {
        GetComponents<Transformation>(transformations);
        if (transformations.Count > 0) //if transformations list has more than zero transformation, ...
        {
            transformation = transformations[0].Matrix; //...then, transformation equals the matrix from first transformation on this list
            for (int i = 1; i < transformations.Count; i++)
            {
                transformation = transformations[i].Matrix * transformation; //...multiplied by all other transformation matrices iteratively.
            }
        }
    }

}