using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

    private Animator ani;
	void Start () {
        ani = GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider other) 
    {
        if (other.name == "enemyAttackColider") 
        {
            Debug.Log("i Got Hit");
            ani.SetTrigger("gotHit");
        }
    
    }
}
