using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{

    [SerializeField] private InputActionReference impulseGrabAction;
    [SerializeField] private AnimationClip closedHandAnimation;
    [SerializeField] private AnimationClip normalHandAnimation;
    [SerializeField] private Transform[] actualFingerTransforms;
    
    private Transform[] animFingerTransforms;
    
    private bool isGrabbing;
    

    void Start()
    {
        animFingerTransforms = new Transform[5]; //5 fingers
        closedHandAnimation.SampleAnimation(gameObject, 0);
        CaptureClosedPose();
        normalHandAnimation.SampleAnimation(gameObject, 0);

    }

    void Update()
    {
        isGrabbing = impulseGrabAction.action.IsPressed();

        if (isGrabbing)
        {
            for (int i = 0; i < 5; i++)
            {
                Transform t = animFingerTransforms[i].GetComponent<Transform>();
                actualFingerTransforms[i].localPosition = t.localPosition;
                actualFingerTransforms[i].localRotation = t.localRotation;
                actualFingerTransforms[i].localScale = t.localScale;
            }
        }

    }
    
    private void CaptureClosedPose()
    {
        for (int i = 0; i < 5; i++) // 5 = number of fingers
        {
            GameObject finger = new GameObject();
            finger.transform.localPosition = actualFingerTransforms[i].localPosition;
            finger.transform.localRotation = actualFingerTransforms[i].localRotation;
            finger.transform.localScale = actualFingerTransforms[i].localScale;
            
            animFingerTransforms[i] = finger.transform;
        }
    }
    
}
