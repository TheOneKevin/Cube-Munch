using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    /* Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

    void OnTriggerEnter(Collider player)
    {
        player.gameObject.transform.position = new Vector3(0, 1, -16);
    }
}
