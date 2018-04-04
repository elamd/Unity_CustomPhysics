using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    public Vector3 velocity;  // avg velocity this fixed update.
    public float mass;
    public bool drawForces = false;

    // Force drawing
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }

    private void Update()
    {
        renderForces();
    }

    private void renderForces()
    {
        if (this.drawForces)
        {
            lineRenderer.enabled = true;
            ApplyForce[] appliedForces = GetComponents<ApplyForce>();
            int numberOfForces = appliedForces.Length;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (ApplyForce applyForce in appliedForces)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -applyForce.forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private Vector3 AddForces()
    {
        Vector3 forceSum = Vector3.zero;
        foreach( ApplyForce applyForce in GetComponents<ApplyForce>())
        {
            forceSum += applyForce.forceVector;
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
