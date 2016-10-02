using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
    public GameObject drop;
    public GameObject deathEffect;

    void OnTriggerEnter(Collider other) 
    {
       // Debug.Log(other.name);
        if (other.name == "attackCollider" && other.isTrigger == true) 
        {
            Destroy(Instantiate(deathEffect,transform.position,transform.rotation)as GameObject, 10);
            //Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/ingame prefabs/metal.prefab", typeof (GameObject)), transform.position, transform.rotation);
            Instantiate(drop, transform.position, transform.rotation);
            Destroy(this.gameObject);
            
        
        }
    }
}
