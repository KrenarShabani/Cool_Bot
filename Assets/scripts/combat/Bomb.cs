using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : missle {

    void OnTriggerEnter(Collider other)
    {
        print(this.GetComponent<SphereCollider>().tag);
        if (other.gameObject.layer == 10)
        {
            print("I hit the ground");
            GetComponentInChildren<SphereCollider>().enabled = true;
        }

    }
}
