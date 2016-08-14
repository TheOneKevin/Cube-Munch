using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public GameObject playerCube;

    /* Use this for initialization
	void Start () {
	
	}*/
	
	// Update is called once per frame
	void Update ()
	{
		if(playerCube.transform.position.y <= -2)
            playerCube.transform.position = new Vector3(0, 1, -16);
    }

    void OnTriggerEnter(Collider player)
    {
        player.gameObject.transform.position = new Vector3(0, 1, -16);
    }
}
