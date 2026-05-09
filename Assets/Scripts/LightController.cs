using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Singleton instance
    private static LightController instance;

    // Public property to access the instance
    public static LightController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LightController>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<LightController>();
                    singletonObject.name = typeof(LightController).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    private Light[] lights;
    [SerializeField] private Material emission;

    private float animationTime;
    private float animationTimer;

    private bool isLightOn = false;

    private Color lastColor;
    //private Color currentColor;
    private Color targetColor;

    // Start is called before the first frame update
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

        lights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        emission.SetColor("_EmissionColor", Color.black);

        foreach (Light l in lights)
        {
            if (!l.transform.CompareTag("Flashlight"))
                l.color = Color.black;
        }
    }

    void Start()
    {
        animationTime = 1.5f;
        animationTimer = 0;

        lastColor = Color.black;
        targetColor = Color.black;
    }


    // Update is called once per frame
    void Update()
    {
        if (animationTimer > 0)
        {
            Color c = GetCurrentColor();

            foreach (Light l in lights)
            {
                if (!l.transform.CompareTag("Flashlight"))
                {
                    l.color = c;
                }
            }

            // Lower the saturation of the material to make it more whitish
            float h, s, v;
            Color.RGBToHSV(c, out h, out s, out v);
            emission.SetColor("_EmissionColor", Color.HSVToRGB(h, s*0.4f, v));


            //float t = 1 - animationTimer / animationTime;
            //Debug.Log("Last Color: " + lastColor + ", targetColor: " + targetColor + ", current (" + t + "): " + c);

            animationTimer -= Time.deltaTime;
        }
    }

    Color GetCurrentColor()
    {
        float t = 1 - animationTimer / animationTime;
        return Color.Lerp(lastColor, targetColor, t);
    }

    public void ChangeLightsSmoothly(Color c)
    {
        lastColor = GetCurrentColor();
        targetColor = c;
        animationTimer = animationTime;
    }
}
