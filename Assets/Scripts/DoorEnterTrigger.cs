using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterTrigger : MonoBehaviour
{
    [SerializeField]
    private Collider triggerToExit;
    private Collider triggerToEnter;
    private AudioSource audioSource;

    void Start()
    {
        triggerToEnter = GetComponent<Collider>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            triggerToEnter.enabled = false;
            triggerToExit.enabled = true;
            audioSource.Play();
        }
    }
}