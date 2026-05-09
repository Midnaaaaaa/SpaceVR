using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CapsuleColliderDriver : MonoBehaviour
{
    [SerializeField]
    private XROrigin _origin;
    private CapsuleCollider _collider;

    private float skinWidth;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        skinWidth = _origin.GetComponent<CharacterController>().skinWidth;
    }

    void FixedUpdate()
    {
        if (_origin == null || _collider == null)
            return;

        var height = Mathf.Clamp(_origin.CameraInOriginSpaceHeight, 0, float.PositiveInfinity);

        Vector3 center = _origin.CameraInOriginSpacePos;
        center.y = (height / 2f) + skinWidth;

        _collider.height = height;
        _collider.center = center;
    }
}
