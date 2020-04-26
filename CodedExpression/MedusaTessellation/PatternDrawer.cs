using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDrawer : MonoBehaviour
{
    public GameObject shapePrefab;

    float shapeRadius = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        MakePattern();
    }

    void MakePattern()
    {
        //for (int x = 0; x < 100; x++)
        //{
        //    for (int y = 0; y < 100; y++)
        //    {
        //        PlaceShape(new Vector3(x,y,0), 7, 1f);
        //    }
        //}

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                float hexx = (float)x * (1.5f*shapeRadius);
                float hexy = (float)y * (Mathf.Sqrt(3f)*shapeRadius)+(x%2 * (Mathf.Sqrt(3f)*shapeRadius / 2f));

                PlaceShape(new Vector3(hexx, hexy, 0), 6, shapeRadius, Mathf.Lerp(0f, shapeRadius/3f, x/10f)); //changing number of sides here (second argument) will still tile using the hexagon based tiling
                //Mathf.Lerp(0f, shapeRadius/3f, x/10f) - this code is the noiseAmt but it increases as x increments, which is how we get more irregular hexagons as we go more to the right hand side
                Debug.Log(Mathf.Lerp(0f, shapeRadius / 3f, x / 10f));
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceShape(Vector3 location, int sides, float size, float noiseAmt) {
        GameObject shape = Instantiate(shapePrefab, location, Quaternion.identity);   //Quaternion.identity is 0,0,0 rotation
        //shape above is the slot for Instantiate to return a gameobject to us
        shape.transform.parent = this.transform; //???
        shape.GetComponent<DrawShape>().MakeShape(sides, size, noiseAmt);

    }

}
