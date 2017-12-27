using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
   // public GameObject drop;
   // public GameObject deathEffect;
    public CharacterController enemyController;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20.0f;
    public Transform player;

    void start() 
    {
        //enemyController = GetComponent<CharacterController>();
    
    
    }
    void update() 
    {
        moveDirection.y -= gravity * Time.deltaTime;
        //moveDirection.x = Vector3.forward.x * 5f;
        enemyController.Move(moveDirection * Time.deltaTime);
        //transform.Translate(moveDirection);
        this.transform.rotation = Quaternion.LookRotation(player.position);
        
    }



    /*
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
    }*/

}
