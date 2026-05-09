using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCast : MonoBehaviour
{
    public AudioClip[] castSound;
    public AudioClip[] castSoundShort;
    bool soundPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!soundPlayed && !collision.transform.CompareTag("Gun"))
        {
            if (GravityController.Instance.GetGravity())
            {
                int randIndex = Random.Range(0, castSound.Length);
                soundPlayed = true;
                AudioSource.PlayClipAtPoint(castSound[randIndex], transform.position, 0.5f);
            }
            else
            {
                int randIndex = Random.Range(0, castSoundShort.Length);
                soundPlayed = true;
                AudioSource.PlayClipAtPoint(castSoundShort[randIndex], transform.position, 0.5f);
            }
        }
    }
}
