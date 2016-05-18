using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float acceleration;
    public float maxSpeed = 5.0f;
    public AudioClip moveSound;
    public float oioioioi;
    public float volumeConst = 0.6f;
    private Vector3 input;
    private AudioSource source;
	// Use this for initialization
	void Start ()
    {
        source = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.y < 0)
            transform.position = new Vector3(0, 1, -16);
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
            GetComponent<Rigidbody>().AddForce(input * acceleration);
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.6 && GetComponent<Rigidbody>().velocity.y == 0)
            source.PlayOneShot(moveSound, volumeConst * GetComponent<Rigidbody>().velocity.magnitude / 8f);
        else if (GetComponent<Rigidbody>().velocity.magnitude <= 0.6 && source.isPlaying)
            source.Stop();
        oioioioi = GetComponent<Rigidbody>().velocity.magnitude;
    }
}
