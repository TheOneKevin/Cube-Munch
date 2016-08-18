using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public GameObject playerCube;
    public Vector3 deathSpot;

    /* Use this for initialization
	void Start () {
	
	}*/
	
	// Update is called once per frame
	void Update ()
	{
        if (playerCube.transform.position.y <= -2)
            playerCube.transform.position = deathSpot;
    }

    void OnTriggerEnter(Collider player)
    {
        player.gameObject.transform.position = deathSpot;
    }
}
