using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandsManager : MonoBehaviour
{

    public Transform prefab;


    // Start is called before the first frame update
    void Start()
    {

        for (int z = 0; z < 8; z++)
        {
            for (int x = 0; x < 8; x++)
            {
                Instantiate(prefab, new Vector3(x * 2.0F, x, z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
