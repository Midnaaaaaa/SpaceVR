using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStar : MonoBehaviour
{
    [SerializeField] private AudioClip starSound;

    public void PlayStarSound()
    {
        AudioSource.PlayClipAtPoint(starSound, transform.position, 0.5f);
    }
}
