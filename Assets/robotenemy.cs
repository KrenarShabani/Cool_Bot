using UnityEngine;
using System.Collections;

public class robotenemy : MonoBehaviour{ 
    private CharacterController enemycontroller;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 20.0f;
    public GameObject route;
    public Animator ani;
    private bool HasSeenPlayer = false;
    public BoxCollider punchCollider;
    public BoxCollider inRangeCollider;
    private bool isAttacking = false;
    private Transform player;
    private NPCHealth healthsys;
    //routes will always have an null transform at the end of the array
    private Transform[] routes;
    
    private int checkpoint = 0;
	// Use this for initialization
	void Start () {
        healthsys = GetComponent<NPCHealth>();
        enemycontroller = GetComponent<CharacterController>();
        routes = route.GetComponentsInChildren<Transform>();
        shiftAll();
    }
	
	// Update is called once per frame
	void Update () {
        moveDirection = Vector3.zero;





        if (!HasSeenPlayer && !ani.GetBool("dead"))
        {
            if (Vector3.Distance(enemycontroller.transform.position, routes[checkpoint].position) < 2)
                setWayPoint();
            //print(checkpoint);
            Vector3 newcheckp = new Vector3(routes[checkpoint].position.x, 0f, routes[checkpoint].position.z);

            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.position,routes[checkpoint].position), .1f * Time.deltaTime);
            transform.LookAt(routes[checkpoint]);

            transform.position = Vector3.MoveTowards(enemycontroller.transform.position, newcheckp, 4.7f * Time.deltaTime);

        }
        else if (ani.GetBool("dead")) {  } //---------------------------------------------- this is unoptomised
        else if (!isAttacking && 
            !ani.GetNextAnimatorStateInfo(0).IsName("attack") &&
            !ani.GetNextAnimatorStateInfo(0).IsName("suprised") && ((float)Vector3.Distance(transform.position,player.position) > 2f) &&
            !ani.GetBool("dead"))
        {
            transform.position = Vector3.MoveTowards(enemycontroller.transform.position, player.position, 10f * Time.deltaTime);
            transform.LookAt(player);
        }
        moveDirection.y -= gravity;
        enemycontroller.Move(moveDirection);
    }

    private void shiftAll() 
    {
        for (int i = 1; i < routes.Length; i++) 
        {
            routes[i - 1] = routes[i];
        }
        routes[routes.Length - 1] = null; 
    }
    private void setWayPoint() 
    {
        if (checkpoint == routes.Length - 2)
            checkpoint = 0;
        else
            checkpoint++;
    
    }

    void OnTriggerEnter(Collider other) 
    {
        if (ani.GetNextAnimatorStateInfo(0).IsName("dead")) return;



        if (other.name == "attackCollider") 
        {
            enemycontroller.SimpleMove(new Vector3(5f,5f,5f));
            healthsys.getHit(5);
        }
        else if (other.name == "bullet(Clone)") 
        {
            healthsys.getHit(10);
            Destroy(other.gameObject, 0.1f);
        }
    }

    public void setAttacking(bool flag) 
    {
        isAttacking = flag;
    }
    public bool getAttacking() 
    {
        return isAttacking;
    }
    public void setSeenPlayer(bool flag) 
    {
        HasSeenPlayer = flag;
    }
    public void setPlayer(Transform pla) 
    {
        player = pla;
    }
}
