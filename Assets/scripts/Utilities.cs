using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour {

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static string[] getCrafts()
    {
        string[] craft;
        string splitter = PlayerPrefs.GetString("inventory");
        craft = new string[PlayerPrefs.GetString("inventory").Split(',').Length - 1];
        craft = splitter.Split(',');
        //foreach (string s in craft) print(s);
        return craft;
    }
}
