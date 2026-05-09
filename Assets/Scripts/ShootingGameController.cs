using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameController : MonoBehaviour
{
    private int numTargets = 3;
    [SerializeField] private GameObject accessCardPrefab;
    [SerializeField] private GameObject gravityInteractablesParent;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private Light light;
    

    public void TargetHit()
    {
        numTargets--;
        if (numTargets == 0)
        {
            Debug.Log("You win!!!");
            GameObject card = Instantiate(accessCardPrefab, transform.position, Quaternion.Euler(0,0,-90f), gravityInteractablesParent.transform);
            card.GetComponent<Rigidbody>().AddForce(new Vector3(0,-1,0));
            card.GetComponent<Rigidbody>().useGravity = GravityController.Instance.GetGravity();
            card.layer = LayerMask.NameToLayer("Card");
            AudioSource.PlayClipAtPoint(winSound, transform.position, 0.5f);
            light.enabled = true;
        }
    }
}
