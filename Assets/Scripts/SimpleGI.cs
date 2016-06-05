using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.ImageEffects;

public class SimpleGI : MonoBehaviour {

    #region Variables

    //The change in sunlight color throughout the day
    public Gradient dayNightLightColor; //Like a stonger-than-ambient ambient light
    //How long  a day is (seconds)
    public float duration = 120;
    public float dayTimeOffset;
    public GameObject sunFlare; //Sun flare
    public GameObject sunCube; //Sun cube
    public string PercentOfDay;
    //Sun lamp to rotate
    public Light mainLight;
    public Camera skyCam; //Camera with the skybox

    public Material a, b, c, d, e, f, NormalDay; //Midday-Dawn, Midday-Dusk, Evening, Midnight, Sunset, Daybreak

    public Transform targetRot1; public Transform targetRot2;

    //public bool isDay; //AI stuff. Use latter

    private float time;
    private float rot = 0;

    #endregion

    #region Unity Defaults

    // Use this for initialization
    void Start ()
    {
        RenderSettings.ambientIntensity = 0.6f; //Set lights
        mainLight.intensity = 0.6f;
        mainLight.transform.Rotate(2 * 120 / duration + dayTimeOffset, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time + dayTimeOffset; //Set variable
        float t = (time % duration) / duration * 100; //Get percentage of day is complete
        PercentOfDay = t + "%"; //Debuggin purposes
        //Check the time (out of 2 mins) and set skybox accordingly
        if (t <= 4) //1 hour out of 42 hours
            setSkyBox(c, 4, 0, true);
        else if (4 < t && t <= 8)
            setSkyBox(f, 4, 4, true);
        else if (8 < t && t <= 23)
            setSkyBox(a, 15, 8, true);
        else if (23 < t && t <= 43)
            skyCam.GetComponent<Skybox>().material = NormalDay;
        else if (43 < t && t <= 58)
            setSkyBox(b, 15, 43);
        else if (58 < t && t <= 62)
            setSkyBox(e, 4, 58);
        else if (62 < t && t <= 66)
            setSkyBox(c, 4, 62);
        else if (66 < t && t <= 100)
            skyCam.GetComponent<Skybox>().material = c;

        //Rotate skybox
        rot += Time.deltaTime * 0.4f;
        a.SetFloat("_Rotation", rot); b.SetFloat("_Rotation", rot);
        c.SetFloat("_Rotation", rot); d.SetFloat("_Rotation", rot);
        e.SetFloat("_Rotation", rot); f.SetFloat("_Rotation", rot);
        NormalDay.SetFloat("_Rotation", rot);
        if (rot >= 360)
            rot = 0;

        //Set the lamp and rotate accordingly
        if (t <= 66)
        {
            if (skyCam.GetComponent<SunShafts>() != null)
                skyCam.GetComponent<SunShafts>().enabled = true;
            //Rotate the lamp 2 degrees per second, over the course of 120 seconds
            //And increase/decrease speed based on the total duration of a day
            mainLight.transform.rotation = Quaternion.RotateTowards(mainLight.transform.rotation, targetRot1.rotation, Time.deltaTime * 2f * 120 / duration);
            sunFlare.transform.position = new Vector3(0, sunFlare.transform.position.y + 0.1f * 120 / duration, 21);
            sunCube.transform.position = new Vector3(0, sunFlare.transform.position.y + 0.1f * 120 / duration, 21.25f);
        }
        else if (t > 66) //Rotate backwards at night
        {
            mainLight.transform.rotation = Quaternion.RotateTowards(mainLight.transform.rotation, targetRot2.rotation, Time.deltaTime * 3f * 120 / duration);
            sunFlare.transform.position = new Vector3(0, -3, 21f);
            sunCube.transform.position = new Vector3(0, -3, 21.25f);
            if (skyCam.GetComponent<SunShafts>() != null)
                skyCam.GetComponent<SunShafts>().enabled = false;
        }
        mainLight.color = dayNightLightColor.Evaluate((time % duration) / duration); //Lamp color
        //Ambient light + Sun flare
        RenderSettings.skybox = skyCam.GetComponent<Skybox>().material;
        sunFlare.GetComponent<LensFlare>().color = dayNightLightColor.Evaluate((time % duration) / duration);
    }

    #endregion

    #region Functions

    /// <summary>
    /// Sets the skybox to a material
    /// </summary>
    /// <param name="sky">A blend skybox material</param>
    /// <param name="dif">The range of t</param>
    /// <param name="indif">The bottom most value for the range of t</param>
    /// <param name="isBackward">Determins whether the value is going from 0-1 (false) or 1-0 (true). Defaults to false.</param>
    private void setSkyBox(Material sky, float dif, float indif, bool isBackward = false)
    {
        float pt = (time % duration) / duration; //Get the decimal percents
        float indiff = indif / 100; float diff = dif / 100;
        //Blend the skyboxes together (lerp) so the transition is smooth and gradual
        if (isBackward)
        {
            skyCam.GetComponent<Skybox>().material = sky;
            skyCam.GetComponent<Skybox>().material.SetFloat("_SkyBlend", Mathf.Clamp(1 - 1 * (pt - indiff) / diff, 0f, 1f));
        }
        else
        {
            skyCam.GetComponent<Skybox>().material = sky;
            skyCam.GetComponent<Skybox>().material.SetFloat("_SkyBlend", Mathf.Clamp(1 * (pt - indiff) / diff, 0f, 1f));
        }
    }

    #endregion

}
