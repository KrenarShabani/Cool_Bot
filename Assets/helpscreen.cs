using UnityEngine;
using System.Collections;

public class helpscreen : MonoBehaviour {
    public Canvas help;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
            toggleScreen();
	}

    public void toggleScreen() 
    {
        if (help.GetComponent<Canvas>().enabled) 
        {
            help.GetComponent<Canvas>().enabled = false;
        }
        else
            help.GetComponent<Canvas>().enabled = true; ;
    }
}
