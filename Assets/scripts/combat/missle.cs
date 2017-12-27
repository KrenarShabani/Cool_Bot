using UnityEngine;
using System.Collections;

public class missle : MonoBehaviour {
    private Transform target;
    public GameObject bullet;
    public float force;
    private bool IsLocked;

    bool onlyOnce = true;
	// Use this for initialization
	void Start () {
        //Destroy(bullet, 5);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (IsLocked && target != null) 
        {
            bullet.GetComponent<Rigidbody>().useGravity = false;
            //print("moveing");
            bullet.transform.position = Vector3.Lerp(bullet.transform.position, target.position + 4f * Vector3.up, 3f * Time.deltaTime);
        }
        else if(onlyOnce)
        {
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward* force);
            onlyOnce = false;
        }
        
	}

    public void setValues(bool isLocked, Transform targ) 
    {
        IsLocked = isLocked;
        target = targ;
    }

   /* void OnTriggerEnter(Collider other)
    {
        print(other.name);

    }*/
}
