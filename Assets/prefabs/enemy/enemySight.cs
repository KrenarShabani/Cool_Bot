using UnityEngine;
using System.Collections;

public class enemySight : MonoBehaviour {
    Component parent;
	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<robotenemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jackle") 
        {
            parent.GetComponent<robotenemy>().ani.SetTrigger("test");
            parent.GetComponent<robotenemy>().setSeenPlayer(true);
            parent.GetComponent<robotenemy>().setPlayer(other.transform);
            parent.GetComponent<robotenemy>().ani.SetBool("enemyseen", true);
            //transform.LookAt(other.transform.position);
        }
        else if (other.name == "bullet(Clone)") 
        {
            GetComponent<SphereCollider>().radius *= 2;
        
        }

    }
}
