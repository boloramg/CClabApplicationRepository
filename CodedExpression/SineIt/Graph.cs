using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph_hw : MonoBehaviour
{
    public Transform pointPrefab; //a unity object, in this case a cube
    [Range(10, 100)]
    public int resolution = 10;
    [Range(10, 100)]
    public int circleResolution = 10;
    Transform[] points; //a container  for an array of objects - the array is called points
    Transform[] circlePoints; //a container  for another array of objects - the array is called circlePoints
    Vector3 scale;
    public int radius = 1;

    float step;
    float circleStep;

    List<Transform[]> cubesList = new List<Transform[]>(); //a container  for a list of arrays of objects - the list is called cubesList

    void Awake()
    {
        points = new Transform[resolution]; //creates an array to hold objects - resolution is how many objects it can hold
        circlePoints = new Transform[circleResolution]; //creates another array to hold objects - circleResolution is how many objects it can hold
        step = 3f / resolution;
        scale = Vector3.one * step;
        Vector3 position; //a Vector3 (i.e. a vector in 3D space) container is declared and named position
        position.y = 0f;
        position.z = 0f;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab); //an object named 'point' is declared and a cube is assigned to it (I manually assign this cube in Unity)
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position; //we're using 'position' (from line 28) as the cube object's position (i.e. localPosition)
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point; //the i object in the 'points' array is now the 'point' object
            for (int k = 0; k < circlePoints.Length; k++)
            {
                Transform circlePoint = Instantiate(pointPrefab); //an object container named 'circlePoint' is declared and a cube is assigned to it 
                Vector3 circlePosition; //a Vector3 container is declared and named circlePosition
                circlePosition.x = point.localPosition.x; //the x value of this Vector3 is equal to the 'point' object's position on the x-axis
                circlePosition.y = position.y; //the y value equals the y value of 'point' object previously declared
                circlePosition.z = position.z; //the z value equals the z value of 'point' object previously declared
                circlePoint.localPosition = circlePosition; //the circlePosition vector is now used as the position of object 'circlePoint'
                circlePoint.localScale = scale;
                circlePoint.SetParent(transform, false);
                circlePoints[k] = circlePoint; //updating the k-th object in the 'circlePoints' array as the 'circlePoint' object
            }
            cubesList.Add(circlePoints); //the circlePoints array with k objects inside is added to a list called cubesList - this list has i number of arrays inside
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i]; //an object named 'point' is declared and we assign the i-th object from 'points' array to it, so that we can work with this specific object locally
            Vector3 position; //a Vector3 container is declared
            position = point.localPosition; //assigning this new Vector3 the same positions as 'point' object
            position.y = Mathf.Sin(Mathf.PI * position.x + Time.time); //the y position moves up and down using sine function
            point.localPosition = position; //assigning 'point' object this new position (with y always updating)
            circleStep = 2 * Mathf.PI / circleResolution;
            circlePoints = cubesList[i];
            //Transform circlePoint; //an object container named circlePoint is declared
            //Vector3 circlePosition; //a Vector3 container is declared

            for (int k = 0; k < circlePoints.Length; k++)
            {
                Transform circlePoint; //an object container named circlePoint is declared
                Vector3 circlePosition; //a Vector3 container is declared
                circlePoint = circlePoints[k]; //the k-th object in the 'circlePoints' array is now assigned to the 'circlePoint' container
                circlePosition = circlePoint.localPosition; //the 'circlePoint' object's position is assigned to 'circlePosition' container 
                circlePosition.y = position.y + Mathf.Sin(circleStep * k + Time.time) * radius; //calculating y of vector circlePosition using original 'points[i]' object's y position
                circlePosition.z = position.z + Mathf.Cos(circleStep * k + Time.time) * radius; //calculating z of vector circlePosition using original 'points[i]' object's y position
                circlePoint.localPosition = circlePosition; //using this new 'circlePosition' vector as 'circlePoint' object's position
                circlePoints[k] = circlePoint; //updating the k-th object in the 'circlePoints' array as the 'circlePoint' object
                //circlePoints[k] = cubesList[i][k]; //updating the k-th object in the 'circlePoints' array as the k-th object in cubesList[i] array
                cubesList[i][k] = circlePoint;
            }
            cubesList[i] = circlePoints; //updating the i-th array in cubesList as the 'circlePoints' array
            Debug.Log(cubesList[i][5].localPosition);

        }
        
    }
}
