using UnityEngine;
using System.Collections;

public class EnemyCube : MonoBehaviour {

    public Transform[] targets;
    public float speed;
    private int currentIndex;

	// Use this for initialization
	void Start ()
    {
        transform.position = targets[0].position;
        currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position == targets[currentIndex].position)
            currentIndex++;
        if (currentIndex >= targets.Length)
            currentIndex = 0;
        transform.position = Vector3.MoveTowards(transform.position, targets[currentIndex].position, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider player)
    {
        //When anything hits, send them back to spawn
        player.gameObject.transform.position = new Vector3(0, 1, -16);
    }
}
