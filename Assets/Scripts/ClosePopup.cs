using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    public EventSystem events;
	/* Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

    public void clickAway()
    {
        GameObject go = events.currentSelectedGameObject;
        go.GetComponent<Image>().enabled = false;
        foreach (var txt in go.GetComponentsInChildren<Text>())
            txt.enabled = false;
    }
}
