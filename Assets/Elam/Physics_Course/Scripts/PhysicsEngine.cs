using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    public List<Vector3> forceVectorList = new List<Vector3>();

    public Vector3 velocity;  // avg velocity this fixed update.
    public float mass;

    private Vector3 AddForces()
    {
        Vector3 forceSum = Vector3.zero;
        foreach( Vector3 force in forceVectorList)
        {
            forceSum += force;
        }
        
        return forceSum;
    }

    private void UpdateVelocity()
    {
        Vector3 acceleration = Vector3.zero;
        if(mass > 0)
            acceleration = AddForces() / mass;
        velocity += acceleration * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
        this.transform.position += velocity * Time.deltaTime;
    }
}
