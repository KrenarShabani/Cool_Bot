using UnityEngine;
using System.Collections;

public class asdasasdasd : MonoBehaviour {
    public CharacterController rapeme;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    rapeme.SimpleMove(new Vector3(1f,1f,0f) * 5f);
        rapeme.Move(Vector3.up * 1f);
	}
}
