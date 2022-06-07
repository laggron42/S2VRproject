using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Allows the Skybox to get the light position
    Used on the main light
*/
public class SkyboxLight : MonoBehaviour
{
    public Renderer sky;
    private Light sun;

    [Header("Day Colors")]
    public Color dayColor;
    public Color dayHorizon;
    public Color daySun;

    [Header("Noon Colors")]
    public Color noonColor;
    public Color noonHorizon;
    public Color noonSun;

    [Header("Night Colors")]
    public Color nightColor;
    public Color nightHorizon;
    public Color nightSun;

    void Start()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
        sky.material.SetColor("_Sky_Color", dayColor);
        sky.material.SetColor("_Horizon_Color", dayHorizon);
        sky.material.SetColor("_Sun_Color", daySun);
        sun = GetComponent<Light>();
        sun.color = daySun;
    }

    void Update()
    {
        
    }
}
