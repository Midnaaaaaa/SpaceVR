using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    private bool isLightOn = false;
    private float animationTime;
    private float animationTimer;
    [SerializeField] private AudioClip audioClip;
    
    [SerializeField] private GameObject handMenu;
    
    void Start()
    {
        animationTime = 0.5f;
        animationTimer = 0;
    }
    
    void Update()
    {
        if (isLightOn && animationTimer > 0)
        {
            float parameter = animationTimer / animationTime;
            ReproduceAnimation(parameter);
            animationTimer -= Time.deltaTime;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        isLightOn = true;
        GetComponent<Collider>().enabled = false;
        animationTimer = animationTime;
        handMenu.SetActive(true);
        LightController.Instance.ChangeLightsSmoothly(Color.white);
        AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.7f);
    }

    void ReproduceAnimation(float parameter)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(0.061f, transform.localPosition.y, parameter), transform.localPosition.z);
    }
    
}
