using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    private IXRSelectInteractor hand;

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    [SerializeField] private GameObject parentOfBullets;
    [SerializeField] private AudioClip sfx;
    bool isGrabbed;
    float cooldown = 1;
    float currentCooldown;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        currentCooldown = 0;
        hand = null;
    }

    void Update()
    {

        currentCooldown -= Time.deltaTime;

        //If you want a different input, change it here
        if (isGrabbed && hand.transform.GetComponent<TriggerValueController>().GetTriggerValue() > 0.6f)
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            if (currentCooldown <= 0 && !gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            {
                // Debug.Log("Animate!");
                gunAnimator.SetTrigger("Fire");
                currentCooldown = cooldown;
            }
        }
    }

    public void setGrabbed(bool grabbed)
    {
        isGrabbed = grabbed;
        hand = grabbed ? GetComponentInParent<GrabInteractableGravity>().GetHand() : null;
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        // Debug.Log("Shoot!");
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation, parentOfBullets.transform);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        bullet.GetComponent<Rigidbody>().useGravity = GravityController.Instance.GetGravity();
        bullet.layer = LayerMask.NameToLayer("Bullet");

        AudioSource.PlayClipAtPoint(sfx, barrelLocation.position, 0.5f);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation, parentOfBullets.transform) as GameObject;

        Rigidbody rb = tempCasing.GetComponent<Rigidbody>();
        rb.useGravity = GravityController.Instance.GetGravity();
        //Add force on casing to push it out
        rb.AddExplosionForce(UnityEngine.Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        rb.AddTorque(new Vector3(0, UnityEngine.Random.Range(100f, 500f), UnityEngine.Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
