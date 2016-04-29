using UnityEngine;
using System.Collections;

public class recipePusher : MonoBehaviour {
    // Use this for initialization
    public string[] recipe;
    public int[] needed;
    public GameObject createe;
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pushRecipie()
    {

        GameObject.FindGameObjectWithTag("itemmanager").GetComponent<recipie>().updateRecipe(recipe, needed, createe);
    }
}
