using UnityEngine;
using System.Collections;

public class missle : MonoBehaviour {
    private GameObject target;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("target");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(target != null)
        bullet.transform.position = Vector3.Lerp(bullet.transform.position, target.transform.position, Time.deltaTime * 2f);
	}
}
