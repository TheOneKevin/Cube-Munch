using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour {

    //Manages scene fading and menu buttons
    public GameObject cam;
    public float camRot;
    public float fadeSpeed = 1.5f;
    public Canvas canvas;
    public Image logo;
    public GUITexture screenOverlay;
    private bool flag;
    private bool goAhead;
    private bool isSceneStarting = true;

	// Use this for initialization
	void Start ()
    {
        screenOverlay.enabled = true;
        flag = false;
        goAhead = false;
        screenOverlay.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isSceneStarting)
            StartScene();
        camRot = cam.transform.rotation.x;
        if (flag)
        {
            float lerp = -Time.deltaTime * 10;
            cam.transform.Rotate(lerp, 0, 0);
        }
        if (cam.transform.rotation.x < -0.40f)
        {
            logo.color = new Color(logo.color.r, logo.color.b, logo.color.g, 100);
        }
        if (cam.transform.rotation.x < -0.63f)
        {
            logo.color = new Color(logo.color.r, logo.color.b, logo.color.g, 0);
            flag = false;
            goAhead = true;
        }
        if (goAhead)
        {
            EndScene("Tutorial");
        }
    }

    #region Scene Fadings
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
        if(screenOverlay.color.a <= 0.05f)
        {
            screenOverlay.color = Color.clear;
            screenOverlay.enabled = false;
            isSceneStarting = false;
        }
    }

    void EndScene(string lvlName)
    {
        screenOverlay.enabled = true;
        FadeToBlack();
        if(screenOverlay.color.a >= 0.95f)
        {
            SceneManager.LoadScene(lvlName);
        }
    }
    #endregion

    #region Button Events
    public void loadLevel(bool isMenu)
    {
        if(isMenu) //Check if tutorial button is clicked
        {
            flag = true;
            canvas.enabled = false;
        }
        else if(!isMenu) //Else, the play button was clicked
        {

        }
        #endregion
    
    }
}
