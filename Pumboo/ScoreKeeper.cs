using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    static public int score;


    // Start is called before the first frame update
    void Start()
    {
        score = PlayheadMover.score;   
    }

    // Update is called once per frame
    void Update()
    {

    }

}
