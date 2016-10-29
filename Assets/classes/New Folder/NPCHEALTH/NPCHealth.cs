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
        ani.SetBool("dead", true);
        StartCoroutine("rmObject");
    }

    public void getHit(int dmg) //----------------------------------------------------needs tweaking
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
    }

    IEnumerator rmObject() { 
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
