using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GravityController : MonoBehaviour
{
    // Singleton instance
    private static GravityController instance;

    // Public property to access the instance
    public static GravityController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GravityController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GravityController>();
                    singletonObject.name = typeof(GravityController).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    public GameObject interactableObjectsParent;

    private Rigidbody[] interactableObjectsRB;
    private bool isGravityOn = true;

    [SerializeField] private GameObject m_climbLocomotion;
    [SerializeField] private GameObject m_moveLocomotion;
    [SerializeField] private XROrigin XROrigin;
    [SerializeField] private GameObject Camera;
    
    [SerializeField] private GameObject zeroGravityInteractables;

    private Rigidbody characterRB;
    private CharacterController characterController;


    void Awake()
    {
        // Ensure there's only one instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        interactableObjectsRB = interactableObjectsParent.GetComponentsInChildren<Rigidbody>();
    }

    void Start()
    {
        characterController = XROrigin.GetComponent<CharacterController>();
        characterRB = XROrigin.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchGravity();
        }

        if (XROrigin.transform.position.y < -50)
        {
            XROrigin.transform.position = new Vector3(0,0,-1.5f);
            characterRB.velocity = Vector3.zero;
        }
    }

    public void SetGravity(bool hasGravity)
    {
        Rigidbody[] newInteractables = interactableObjectsParent.GetComponentsInChildren<Rigidbody>();
        if (newInteractables.Length > interactableObjectsRB.Length) //If new interactables have been instantiated we have to update the list of rigidbodies to take in account this new objects
        {
            interactableObjectsRB = newInteractables;
        }
        foreach (Rigidbody rb in interactableObjectsRB)
        {
            rb.useGravity = hasGravity;
        }

        // Disable climb locomotion and enable move locomotion, disable XROrigin RigidBody, and Camera collider and activate XROrigin character controller 
        m_climbLocomotion.GetComponent<ZeroGravityLocomotion>().enabled = !hasGravity;
        //m_moveLocomotion.GetComponent<DynamicMoveProvider>().enabled = hasGravity;
        m_moveLocomotion.GetComponent<DynamicMoveProvider>().disabled = !hasGravity;
        
        XROrigin.GetComponent<Rigidbody>().isKinematic = hasGravity;
        XROrigin.GetComponent<CharacterController>().enabled = hasGravity;
        Camera.GetComponent<CapsuleCollider>().enabled = !hasGravity;

        // Disable the zero gravity interactable script from the scene
        zeroGravityInteractables.GetComponent<ZeroGravityInteractable>().enabled = !hasGravity;

        isGravityOn = hasGravity;
        
        if (!isGravityOn)
        {
            characterRB.velocity = characterController.velocity / 2;

            LightController.Instance.ChangeLightsSmoothly(Color.red);
        }
        else
        {
            LightController.Instance.ChangeLightsSmoothly(Color.white);
        }
    }

    public void SwitchGravity()
    {
        SetGravity(!isGravityOn);
    }

    public bool GetGravity()
    {
        return isGravityOn;
    }



    
}
