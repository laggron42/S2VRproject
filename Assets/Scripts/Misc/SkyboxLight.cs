using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Allows the Skybox to get the light position
    Used on the main light
*/
public class SkyboxLight : MonoBehaviour
{
    void Start()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
    }
}
