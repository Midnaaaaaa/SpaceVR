using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : MonoBehaviour
{
    private IXRSelectInteractor hand;
    bool isGrabbed;
    Light light;

    float currentCooldown;
    float cooldown = 0.3f;

    [SerializeField] private AudioClip audiobutton;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
        currentCooldown = 0;
        hand = null;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        
        //If you want a different input, change it here
        if (isGrabbed && hand.transform.GetComponent<TriggerValueController>().GetTriggerValue() > 0.6f)
        {
            if (currentCooldown <= 0)
            {
                AudioSource.PlayClipAtPoint(audiobutton, transform.position, 0.5f);
                light.enabled = !light.enabled; 
                currentCooldown = cooldown;
            }
        }
    }
    
    public void setGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;
        hand = grabbed ? GetComponent<GrabInteractableGravity>().GetHand() : null;
    }
}
