using UnityEngine;
using System.Collections;

public class punchCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            //GetComponent<CharacterController>().SimpleMove(new Vector3(0f,100f,0f) * 1000f);// this doesnt work
            other.GetComponentInParent<NPCHealth>().getHit(5);
        }
    }
}
