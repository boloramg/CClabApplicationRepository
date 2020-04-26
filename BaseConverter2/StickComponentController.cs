using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickComponentController : MonoBehaviour
{
    // Instantiate a prefab with an attached StickGenerator script
    public StickGenerator stickPrefab;

    void Start()
    {
        //float angle = 360 / 8;
        // Instantiate the missile at the position and rotation of this object's transform
      //  StickGenerator stick = (StickGenerator)Instantiate(stickPrefab, transform.position, transform.Rotate(0, angle, 0, Space.World));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
