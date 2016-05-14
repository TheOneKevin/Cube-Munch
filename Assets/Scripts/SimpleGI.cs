using UnityEngine;
using System.Collections;
using System;

public class SimpleGI : MonoBehaviour {
    //The change in sunlight color throughout the day
    public Gradient dayNightLightColor; //Like a stonger-than-ambient ambient light
    //How long  a day is (seconds)
    public float duration = 120;
    public float dayTimeOffset; //Not used
    public string PercentOfDay;
    private float time;
    /* Not used yet
    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;

    public float maxAmbientIntensity = 1f;
    public float minAmbientIntensity = 0f;
    public float minAmbientPoint = -0.2f; */
    //Sun lamp to rotate
    public Light mainLight;
    public Camera skyCam; //Camera with the skybox

    public Material a, b, c, d, e, f, NormalDay; //Midday-Dawn, Midday-Dusk, Evening, Midnight, Sunset, Daybreak

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
        else if(23 < t && t <= 43)
            skyCam.GetComponent<Skybox>().material = NormalDay;
        else if (43 < t && t <= 58)
            setSkyBox(b, 15, 43);
        else if (58 < t && t <= 62)
            setSkyBox(e, 4, 58);
        else if (62 < t && t <= 66)
            setSkyBox(c, 4, 62);
        else if (66 < t && t <= 100)
            skyCam.GetComponent<Skybox>().material = c;

        //Set the lamp and rotate accordingly
        if (t <= 66)
        {
            //Rotate the lamp 2 degrees per second, over the course of 120 seconds
            //And increase/decrease speed based on the total duration of a day
            mainLight.transform.Rotate(Time.deltaTime * 2 * 120 / duration, 0, 0);
        }
        else if (t > 66) //Rotate backwards at night
        {
            mainLight.transform.Rotate(-Time.deltaTime * 3.9f * 120 / duration, 0, 0);
        }
        mainLight.color = dayNightLightColor.Evaluate((time % duration) / duration);

        //Slowly rotate skybox camera
        skyCam.transform.Rotate(0, Time.deltaTime * 0.4f, 0);
        //Ambient light
        RenderSettings.skybox = skyCam.GetComponent<Skybox>().material;
    }

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
            skyCam.GetComponent<Skybox>().material.SetFloat("_Blend", Mathf.Clamp(1 - 1 * (pt - indiff) / diff, 0f, 1f));
        }
        else
        {
            skyCam.GetComponent<Skybox>().material = sky;
            skyCam.GetComponent<Skybox>().material.SetFloat("_Blend", Mathf.Clamp(1 * (pt - indiff) / diff, 0f, 1f));
        }
    }
}
