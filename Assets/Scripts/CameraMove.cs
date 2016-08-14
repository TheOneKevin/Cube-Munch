using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public GameObject cam;
    public GameObject player;
    float temp;

	// Use this for initialization
	void Start ()
    {
        temp = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*if (player.transform.position.y > temp)
            cam.transform.Translate(Vector3.up);
        else if (player.transform.position.y < temp)
            cam.transform.Translate(Vector3.down);
        temp = player.transform.position.y;*/
        cam.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y + 3, cam.transform.position.z);
    }
}
