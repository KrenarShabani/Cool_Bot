using UnityEngine;
using System.Collections;

public class ingameItem : MonoBehaviour {
    public GameObject item;
    public int tagset;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other) 
    {
       // Debug.Log(other.name);
        if (other.name == "Jackle") 
        {
            if (!charinventory.isFull())
            {
                charinventory.addItem(item.gameObject.name);
                Destroy(item);
            }
         }
    }
}
