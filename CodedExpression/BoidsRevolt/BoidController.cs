using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject boidPrefab;
    public List<BoidBehavior> boids = new List<BoidBehavior>();
    public int numOfBoids;
    public float boidSpeed, boidSeparationDist;
    public Transform swarmTarget;

    [Range(0f,1f)] public float weight_alignment;
    [Range(0f,1f)] public float weight_cohesion;
    [Range(0f,1f)] public float weight_separation;
    [Range(0f,1f)] public float weight_target;

    void Start()
    {
        for(int i = 0; i < numOfBoids; i++){
         CreateBoid();
        }
    }

    void CreateBoid(){
        GameObject newboid = Instantiate(boidPrefab, this.transform.position, Quaternion.identity);
        newboid.transform.parent = this.transform;
        BoidBehavior newboidbehavior = newboid.GetComponent<BoidBehavior>();
        newboidbehavior.boidManager = this;
        newboidbehavior.speed = boidSpeed;

        boids.Add(newboidbehavior);

    }

}
