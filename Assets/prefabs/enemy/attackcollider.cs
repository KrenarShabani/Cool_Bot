using UnityEngine;
using System.Collections;

public class attackcollider : MonoBehaviour {

    void OnTriggerEnter(Collider other) 
    {
        if (other.name == "Jackle") 
        {
            GetComponentInParent<Animator>().SetTrigger("inRange");
        }
    }
}
