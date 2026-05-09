using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetController : MonoBehaviour
{
    private ShootingGameController sgc;
    private bool inactiveTarget = false;
    private float animationTimer = 0;
    private float animationTime = 2;
    private Quaternion startRotation;
    public AudioClip[] targetSounds;
    
    void Start()
    {
        sgc = GetComponentInParent<ShootingGameController>();
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (animationTimer < 0)
        {
            ReproduceAnimation();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (animationTimer >= 0)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                
                if (!inactiveTarget)
                {
                    sgc.TargetHit();
                    inactiveTarget = true;
                }
                animationTimer = -animationTime;
                
                int randIndex = Random.Range(0,targetSounds.Length);
                AudioSource.PlayClipAtPoint(targetSounds[randIndex], transform.position, 1f);
            }
        }
    }

    private void ReproduceAnimation()
    {
        float rotationAngle = 30 * (-animationTimer / animationTime) * Mathf.Sin(2 * Mathf.PI * animationTimer);
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, new Vector3(1, 0, 0));

        // Apply the rotation relative to the starting rotation
        transform.rotation = startRotation * rotation;
        animationTimer += Time.deltaTime;
    }
}
