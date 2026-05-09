using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        animator.SetFloat("Trigger", triggerValue);
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        animator.SetFloat("Grip", gripValue);
    }

    public void MakeHandVisible()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }

    public void MakeHandNotVisible()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

}
