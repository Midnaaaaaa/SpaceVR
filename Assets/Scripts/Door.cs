using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<DoorLock> locks;
    private int remainingLocks;
    private Animator animator;
    [SerializeField] private AudioClip doorSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < locks.Count; ++i) 
        {
            locks[i].SetDoor(this, i);
        }

        remainingLocks = locks.Count;

        if (remainingLocks == 0)
        {
            UnlockDoor();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UnlockDoor();
        }
    }

    public void UnlockLock(int lockID)
    {
        if (locks[lockID].Unlocked)
        {
            remainingLocks--;
            if (remainingLocks <= 0)
            {
                UnlockDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", true);
            if(animator.GetBool("unlocked")) AudioSource.PlayClipAtPoint(doorSound, transform.position, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", false);
            if(animator.GetBool("unlocked")) AudioSource.PlayClipAtPoint(doorSound, transform.position, 0.5f);
        }
    }



    private void UnlockDoor()
    {
        Debug.Log("Abrir puerta");
        animator.SetBool("unlocked", true);
    }
}
