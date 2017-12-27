using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    Transform obj;
	// Use this for initialization
	void Start () {
        obj = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        obj.Rotate(0f, 3f, 0f);
	}
}
