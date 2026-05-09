using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    private Door door;
    private int id;
    public bool Unlocked { get; private set; }

    private Material material;

    public AudioClip unlockSound;

    // Start is called before the first frame update
    void Start()
    {
        Unlocked = false;
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDoor(Door d, int id)
    {
        this.door = d;
        this.id = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Unlocked && other.gameObject.layer == LayerMask.NameToLayer("Card"))
        {
            CardKey ck = other.GetComponent<CardKey>();
            if (ck.id == id)
            {
                Unlocked = true;
                door.UnlockLock(id);
                material.SetColor("_EmissionColor", Color.green);
                AudioSource.PlayClipAtPoint(unlockSound, transform.position, 0.5f);
            }
        }
    }
}
