using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    public GameObject musicEmitter;
    private AudioSource musicSource;
    public AudioClip music1, music2, music3, menuMusic;
    public float volumeConst;
    public bool isMenuMusic;

	// Use this for initialization
	void Start ()
    {
        if (isMenuMusic)
        {
            musicSource = musicEmitter.GetComponent<AudioSource>();
            musicSource.PlayOneShot(menuMusic, volumeConst);
        }
        else
        {
            musicSource = musicEmitter.GetComponent<AudioSource>();
            musicSource.PlayOneShot(music1, volumeConst);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
