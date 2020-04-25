using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    
    Vector3 velocity;
    public BoidController boidManager;
    public float speed;
    void Start()
    {
        velocity = new Vector3(Random.value, Random.value, Random.value);
    }

    

    void Update()
    {
        Vector3 currentPosition = this.transform.position;

        //Declare the 3 boid constants
        Vector3 cohesion = boidManager.transform.position; //Vector for keeping boids together
        Vector3 alignment = Vector3.zero; //Vector3 for tracking alignment of boids
        Vector3 separation = Vector3.zero; //Vector3 for tracking separation from neighboring boids
        Vector3 targetseek = boidManager.swarmTarget.position - this.transform.position;

        foreach(BoidBehavior boid in boidManager.boids){
            if(boid == this) continue;

            cohesion += boid.transform.position;
            alignment += boid.velocity;

            Vector3 diffDirec = this.transform.position - boid.transform.position;
            if(diffDirec.magnitude > 0 && diffDirec.magnitude < boidManager.boidSeparationDist){
                separation += boidManager.boidSeparationDist * (diffDirec.normalized / diffDirec.magnitude);
            }
        }

        alignment /= boidManager.boids.Count;
        cohesion /= boidManager.boids.Count;
        cohesion = (cohesion - this.transform.position).normalized; //why is this normalized?
        separation /= boidManager.boids.Count;

        Vector3 newvelocity = Vector3.zero;

        newvelocity += alignment * boidManager.weight_alignment;
        newvelocity += cohesion * boidManager.weight_cohesion;
        newvelocity += separation * boidManager.weight_separation;
        newvelocity += targetseek * boidManager.weight_target;
        newvelocity.Normalize();

        velocity = Limit((velocity + newvelocity)/2f, boidManager.boidSpeed); //is average of the 2 best? Doesnt have to be /2

        //make boid point where its going
        this.transform.up = Vector3.Lerp(this.transform.up, velocity.normalized, Time.deltaTime * 5f);

        //Move the boid
        this.transform.position = currentPosition + velocity * (speed * Time.deltaTime); 
    }

    Vector3 Limit(Vector3 v, float max){
        if(v.magnitude > max){
            return v.normalized * max;
        } else {
            return v;
        }
    }
}
