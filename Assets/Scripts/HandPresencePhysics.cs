using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform handController;
    private Rigidbody rb;
    
    private Quaternion initialRotation;
    
    private Collider[] handColliders;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = handController.GetComponentsInChildren<Collider>();
        initialRotation = transform.rotation;
    }

    public void EnableColliders()
    {
        foreach (Collider c in handColliders) c.enabled = true;
    }

    public void EnableCollidersDelay(float delay)
    {
        Invoke("EnableColliders", delay);
    }

    public void DisableColliders()
    {
        foreach (Collider c in handColliders) c.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Salio " + other.name + " (this is " + this + ")");
    }



    private void FixedUpdate()
    {
        rb.velocity = (handController.position - transform.position) / Time.fixedDeltaTime;

        // Calculate the target rotation by combining the handController rotation with the initial local rotation
        Quaternion targetRotation = handController.rotation * initialRotation;

        // Calculate the difference between the current rotation and the target rotation
        Quaternion rotationDifference = targetRotation * Quaternion.Inverse(transform.rotation);

        // Convert the rotation difference to angular velocity
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);

        // Calculate the required angular velocity in degrees per second
        Vector3 rotationDifferenceInDegree = angle * axis;

        // Apply the angular velocity considering Time.fixedDeltaTime
        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}
