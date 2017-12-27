using UnityEngine;
using System.Collections;

public class NPCHealth : MonoBehaviour {
    public int health;
    Animator ani;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Die() 
    {
        GameObject.Find("Jackle").GetComponentInChildren<lockOnAssists>().hasDied(this.gameObject);
        ani.SetBool("dead", true);
        Destroy(this.gameObject, 3);
    }

    public void getHit(int dmg) 
    {
        health -= dmg;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            ani.SetTrigger("flinched");
        }
        //print(this.name + " " + health);
    }

}
