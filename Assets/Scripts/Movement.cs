using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float acceleration;
    public float maxSpeed = 5.0f;
    private Vector3 input;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y < 0)
            transform.position = new Vector3(0, 1, -16);
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
            GetComponent<Rigidbody>().AddForce(input * acceleration);
	}
}
