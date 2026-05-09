using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class MyTargetEvaluator : XRTargetEvaluator
{
    [Tooltip("The maximum distance from the Interactor. Any target from this distance will receive a 0 normalized score.")]
    [SerializeField]
    float m_MaxDistance = 1f;

    /// <summary>
    /// The maximum distance from the Interactor.
    /// Any target from this distance will receive a <c>0</c> normalized score.
    /// </summary>
    public float maxDistance
    {
        get => m_MaxDistance;
        set => m_MaxDistance = value;
    }

    /// <inheritdoc />
    public override void Reset()
    {
        base.Reset();
        weight = new AnimationCurve(new Keyframe(0, 0, 0, 0.5f), new Keyframe(1, 1, 2, 2));
    }

    /// <inheritdoc />
    /// <remarks>
    /// This is similar to the implementation of the default algorithm to get valid targets in <see cref="XRDirectInteractor"/>.
    /// </remarks>
    protected override float CalculateNormalizedScore(IXRInteractor interactor, IXRInteractable target)
    {
        if (Mathf.Approximately(m_MaxDistance, 0f))
            return 0f;

        
        var baseInteractor = interactor as XRBaseInteractor;
        float distanceSqr;
        if (target is XRBaseInteractable baseInteractable && baseInteractor != null)
        {
#pragma warning disable 618 // Calling deprecated method to help with backwards compatibility with existing user code.
            distanceSqr = baseInteractable.GetDistanceSqrToInteractor(baseInteractor);
#pragma warning restore 618
        }
        else
        {
            distanceSqr = target.GetDistanceSqrToInteractor(interactor);
        }

        //Debug.Log("Distancesqr of " + target.transform.name + ": " + distanceSqr + "(" + target.transform.position + ")" + interactor.transform.name + interactor.transform.position);

        float distScore = 1f - Mathf.Clamp01(distanceSqr / (m_MaxDistance * m_MaxDistance));

        int isGrabbable = target.transform.GetComponent<GrabInteractableGravity>() != null ? 1 : 0;

        //Debug.Log("Score of " + target.transform.name + ": " + distScore * 0.5f + isGrabbable * 0.5f + ", " + isGrabbable);
        return distScore * isGrabbable;
    }
}
