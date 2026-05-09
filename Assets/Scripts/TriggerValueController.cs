using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerValueController : MonoBehaviour
{
    public InputActionProperty triggerValueAction;
    
    private float triggerValue;


    private void FixedUpdate()
    {
        triggerValue = triggerValueAction.action.ReadValue<float>();
    }


    public float GetTriggerValue()
    {
        return triggerValue;
    }
}
