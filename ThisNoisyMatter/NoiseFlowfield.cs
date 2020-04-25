using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibNoise;
using LibNoise.Generator;

public class NoiseFlowfield : MonoBehaviour
{
    Perlin pNoise; //reference to Perlin library
    public int seedValue; //seed value of Perlin noise
    public Vector3Int gridSize; //size of grid 
    public float increment; //resolution of noise applied to the grid
    public Vector3 offset; //vectors that will allow the noise in the grid to change over time

    // Start is called before the first frame update
    void Start()
    {
        pNoise = new Perlin(4, 2, 0.2, 6, seedValue, QualityMode.Medium); //instantiating Perlin noise with seedValue
    }

    // Update is called once per frame
    void Update()
    {
        float scaler = 5f;
        float adjustedTime = Time.deltaTime / scaler; //adjusting Time.deltaTime to be smaller
        offset.x += adjustedTime; //applying this adjusted time-per-frame to move/offset the gizmos 
        offset.z += adjustedTime;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            

            //creating 3 dimensional grid with 3 different for loops inside each other
            float xOff = 0f;
            for (int x = 0; x < gridSize.x; x++)
            {
                float yOff = 0f;
                for (int y = 0; y < gridSize.y; y++)
                {
                    float zOff = 0f;
                    for (int z = 0; z < gridSize.z; z++)
                    {
                        //giving x, y, z values to Perlin noise generator to receive float value back
                        //1 is added to make values all positive
                        float noise = (float)pNoise.GetValue(xOff + offset.x, yOff + offset.y, zOff + offset.z) + 1;
                        //Debug.Log("value: "+ noise);
                        //creating a direction from noise value using sin and cos
                        Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                        //Debug.Log(noiseDirection);
                        //Gizmos.color = new Color(1, 1, 1, noise*0.5f);
                        //Gizmos.color = Color.white;
                        Color color;
                        //using the direction generated to create a color that is then applied to the gizmo
                        color = new Color(noiseDirection.normalized.y, -noiseDirection.normalized.y, -noiseDirection.normalized.x, 1f);
                        Gizmos.color = color;
                        Vector3 pos = new Vector3(x, y, z) + transform.position;
                        //Vector3 endPos = pos + Vector3.Normalize(noiseDirection);
                        Vector3 endPos = pos + noiseDirection * 5;
                        Vector3 size = new Vector3(1, 1, 1);
                        //Gizmos.DrawCube(pos,size);
                        Gizmos.DrawLine(pos, endPos);
                        Gizmos.DrawSphere(endPos, 0.5f);
                        zOff += increment;
                    }
                    yOff += increment;
                }
                xOff += increment;
            }
        }
    }
}
