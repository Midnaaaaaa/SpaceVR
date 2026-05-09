using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityController : MonoBehaviour
{
    public InputActionProperty deviceVelocity;
    
    private Vector3 velocity;


    private void FixedUpdate()
    {
        velocity = deviceVelocity.action.ReadValue<Vector3>();
    }


    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
