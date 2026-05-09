using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button button;
    [SerializeField] private AudioClip buttonSound;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.image.color = GravityController.Instance.GetGravity() ? button.colors.normalColor : button.colors.pressedColor;
    }

    public void ChangeButtonState()
    {
        button.image.color = GravityController.Instance.GetGravity() ? button.colors.normalColor : button.colors.pressedColor;
        AudioSource.PlayClipAtPoint(buttonSound, transform.position, 0.5f);
    }
}
