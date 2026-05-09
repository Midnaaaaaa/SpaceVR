using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitTrigger : MonoBehaviour
{
    private Collider triggerToExit;
    [SerializeField]
    private Collider triggerToEnter;
    private AudioSource audioSource;

    void Start()
    {
        triggerToExit = GetComponent<Collider>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            triggerToEnter.enabled = true;
            triggerToExit.enabled = false;
            audioSource.Stop();
        }
    }
}