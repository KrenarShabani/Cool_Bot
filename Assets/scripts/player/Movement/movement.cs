using UnityEngine;
using System.Collections;
using System;

public class movement : MonoBehaviour {
    public float moveSpeed = 2;
    public float rotateSpeed = 5;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            gameObject.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            gameObject.transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
        } 

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            rb.AddRelativeForce(Vector3.forward * moveSpeed - rb.velocity);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            rb.AddRelativeForce(Vector3.back * moveSpeed);
        }
    }

}
