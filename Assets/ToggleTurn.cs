using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTurn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject turnProvider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            turnProvider.SetActive(!turnProvider.activeSelf);
        }
    }
}
