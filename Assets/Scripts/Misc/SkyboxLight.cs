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

    public float daySpeed = 1.0f;
    private float time = 143.0f;

    private bool atNight = false;

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
        time += Time.deltaTime * daySpeed;

        if (time >= 190)
        {
            time = -10;
            atNight = !atNight;

            if (atNight)
            {
                sun.color = nightSun;
                sky.material.SetColor("_Sun_Color", Color.white);
            }
            else
            {
                sun.color = noonSun;
                sky.material.SetColor("_Sun_Color", noonSun);
            }
        }

        transform.rotation = Quaternion.Euler(time, 166.0f, 177.0f);
        Shader.SetGlobalVector("_SunDirection", transform.forward);

        if (time >= 150)
        {
            float delta = 1 + (150 - time) / 40;

            if (time >= 170)
                sun.intensity = 1 + (170 - time) / 20;

            if (!atNight)
            {
                sky.material.SetColor("_Sky_Color", Color.Lerp(noonColor, dayColor, delta));
                sky.material.SetColor("_Horizon_Color", Color.Lerp(noonHorizon, dayHorizon, delta));
                sky.material.SetColor("_Sun_Color", Color.Lerp(noonSun, daySun, delta));
                sun.color = Color.Lerp(noonSun, daySun, delta);
            }
            else
            {
                sky.material.SetColor("_Sky_Color", Color.Lerp(noonColor, nightColor, delta));
                sky.material.SetColor("_Horizon_Color", Color.Lerp(noonHorizon, nightHorizon, delta));
                sun.color = Color.Lerp(noonSun, nightSun, delta);
            }
        }

        if (time <= 30)
        {
            float delta = 1 - (30 - time) / 40;

            if (time <= 10)
                sun.intensity = (10 + time) / 20;

            if (!atNight)
            {
                sky.material.SetColor("_Sky_Color", Color.Lerp(noonColor, dayColor, delta));
                sky.material.SetColor("_Horizon_Color", Color.Lerp(noonHorizon, dayHorizon, delta));
                sky.material.SetColor("_Sun_Color", Color.Lerp(noonSun, daySun, delta));
                sun.color = Color.Lerp(noonSun, daySun, delta);
            }
            else
            {
                sky.material.SetColor("_Sky_Color", Color.Lerp(noonColor, nightColor, delta));
                sky.material.SetColor("_Horizon_Color", Color.Lerp(noonHorizon, nightHorizon, delta));
                sun.color = Color.Lerp(noonSun, nightSun, delta);
            }
        }
    }
}
