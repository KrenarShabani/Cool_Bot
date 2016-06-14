using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
    public GameObject target;
    private Vector3 positionOffset = Vector3.zero;
	// Use this for initialization
	void Start () {
        positionOffset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
       
        transform.LookAt(target.transform);
        transform.position = new Vector3(target.transform.position.x + positionOffset.x, transform.position.y, target.transform.position.z + positionOffset.z);
	}
}
