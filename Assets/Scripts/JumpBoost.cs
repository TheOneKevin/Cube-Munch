using UnityEngine;
using System.Collections;

public class JumpBoost : MonoBehaviour
{
    public float thrust;
    /* Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
    void OnTriggerEnter(Collider player)
    {
        player.GetComponent<Rigidbody>().AddForce(0, thrust, 0, ForceMode.Force);
    }
}
