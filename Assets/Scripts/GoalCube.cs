using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalCube : MonoBehaviour {

    public string levelName;
    public GUITexture screenOverlay;
    public float fadeSpeed;
    private bool isSceneStarting = true;
    private bool goAhead = false;

	// Use this for initialization
	void Start ()
    {
        screenOverlay.enabled = true;
        screenOverlay.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(isSceneStarting)
            StartScene();
        if(goAhead)
            EndScene(levelName);
    }

    void OnTriggerEnter(Collider player)
    {
        //Move on to next level
        goAhead = true;
    }

    void EndScene(string lvlName)
    {
        screenOverlay.enabled = true;
        FadeToBlack();
        if (screenOverlay.color.a >= 0.95f)
        {
            SceneManager.LoadScene(lvlName);
        }
    }

    void FadeToClear()
    {
        screenOverlay.color = Color.Lerp(screenOverlay.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        screenOverlay.color = Color.Lerp(screenOverlay.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (screenOverlay.color.a <= 0.05f)
        {
            screenOverlay.color = Color.clear;
            screenOverlay.enabled = false;
            isSceneStarting = false;
        }
    }
}
