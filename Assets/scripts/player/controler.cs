using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controler : MonoBehaviour
{
    //public Transform lowerbody;
    
    public float speed = 10.0f;
    float jumpSpeed = 14.0f;
    float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;
    public Camera gCam;
    public Animator ani;
    private BoxCollider punchCollider;
    public GameObject bomber;
    private Quaternion preserve = new Quaternion();
    // Use this for initialization
    void Start()
    {

        Component[] test;
        test = GetComponentsInChildren<BoxCollider>();
        //print(test.Length);
        //print(test[0].name + " " + test[1].name);
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        punchCollider = (BoxCollider)test[1];

    }

    // Update is called once per frame
    void Update()
    {
        //punchCollider.transform.position += new Vector3(0, 0, .1f);
       // punchCollider.transform.position += new Vector3(0, 0, -.1f);
        forward = gCam.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        right = new Vector3(forward.z, 0, -forward.x);

        if (controller.isGrounded)
        {
            ani.SetBool("isInTheAir", false);

            //  ani.Play("Default Take" , -1, 0f);
            // moveDirection = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).normalized;
            //transform.rotation.SetLookRotation(right);
            moveDirection.x = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).x * speed;
            moveDirection.z = (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right).z * speed;

            //o moveDirection = transform.TransformDirection(moveDirection);
            // transform.rotation = moveDirection;
            //  moveDirection = moveDirection * speed;

            if (Input.GetButtonDown("Jump") && !ani.GetCurrentAnimatorStateInfo(0).IsName("punching") && !ani.IsInTransition(0))
            {
                //Debug.Break();
                moveDirection.y = jumpSpeed;
                ani.SetBool("isInTheAir", true);
            }

           // if (Input.GetKeyDown(KeyCode.F) && !ani.GetCurrentAnimatorStateInfo(0).IsName("punching") && ani.GetAnimatorTransitionInfo(0).normalizedTime < 0.10f )
           // {
            //    ani.SetTrigger("punch");
                //punchCollider.isTrigger = true;
           // }
        

            
        }
        else {
            //moveDirection = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward+ Vector3.up*moveDirection.y).normalized;
            moveDirection.x = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).x * speed;
            moveDirection.z = (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right).z * speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        //transform.rotation.Set(0, 0, 0, 0);

        if (  ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ) || ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )  )  
        {
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName("punching") && !ani.GetBool("ratatat"))
            {
                transform.rotation = Quaternion.Lerp(preserve, Quaternion.LookRotation(Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward), 12f * Time.deltaTime);
            }

           ani.SetBool("ismoving", true);
           // controller.Move(moveDirection * Time.deltaTime);
           // ani.Play("walking", -1, 0f);
        }
        else
        {
            transform.rotation = preserve;
            ani.SetBool("ismoving", false);
           // ani.Play("idle", -1, 0f);
        }

        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("punching")) 
        { 
            controller.Move(moveDirection * Time.deltaTime);
        }
        if (ani.GetBool("ratatat")) 
        {
            Quaternion shootrot = new Quaternion(0f, gCam.transform.rotation.y, 0f, gCam.transform.rotation.w);  //--------------------needs tweaking
            transform.rotation = Quaternion.Lerp(transform.rotation,shootrot, 1f);
            //transform.rotation = shootrot;

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.A))
                ani.SetBool("left press", true);
            if (Input.GetKey(KeyCode.D)) 
            {
                ani.SetBool("right press", true);
            }
        }
        else
        {
            ani.SetBool("right press", false);
            ani.SetBool("left press", false);
        }


        //print(transform.rotation + " " + Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));
       // preserve = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        preserve = transform.rotation;

        //transform.rotation.SetLookRotation((Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward));
        if (Input.GetKey(KeyCode.I))
            SceneManager.LoadScene("inventory");
        ani.SetFloat("InputH", Input.GetAxis("Horizontal"));
        ani.SetFloat("InputV", Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (ani.GetBool("bomber"))
            {
                ani.SetBool("bomber", false);
                bomber.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                ani.SetBool("bomber", true);
                bomber.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void setPunchBool(bool flag) 
    {
        punchCollider.isTrigger = flag;
        punchCollider.enabled = flag;
    }

            
            

}