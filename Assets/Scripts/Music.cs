using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    public GameObject musicEmitter;
    private AudioSource musicSource;
    public AudioClip music1, music2, music3;
    public float volumeConst;

	// Use this for initialization
	void Start ()
    {
        musicSource = musicEmitter.GetComponent<AudioSource>();
        musicSource.PlayOneShot(music1, volumeConst);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
