using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
   // private SphereCollider sphere;
	// Use this for initialization
	void Start () {
     //   sphere = GetComponentInChildren<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) 
    {
        print(other.name);
        if (other.tag == "enemy") 
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 5f);
            print(other.name);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(60f, gameObject.transform.position, 5f, 5f, ForceMode.Impulse);
                    //hit.GetComponentInParent<NPCHealth>().getHit(5);
                }
            }
            Destroy(gameObject);
        }
    
    }
}
