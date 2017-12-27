using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonate : MonoBehaviour {
    private ParticleSystem debris; 
    public SphereCollider Blastradius;

    void Awake() 
    {
        debris = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        //print(this.GetComponent<SphereCollider>().tag);
        if (other.gameObject.layer == 10 || other.gameObject.tag == "enemy")
        {
            //print("I hit the ground");

            Blastradius.enabled = true;
            debris.Play();
            GetComponentInParent<SphereCollider>().GetComponentInParent<MeshRenderer>().enabled = false;
            Destroy(GetComponentInParent<SphereCollider>().GetComponentInParent<missle>().bullet,0.5f);
        }
    }
}
