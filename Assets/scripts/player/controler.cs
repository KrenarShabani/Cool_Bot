using UnityEngine;
using System.Collections;

public class controler : MonoBehaviour
{
    public float speed = 10.0f;
    float jumpSpeed = 8.0f;
    float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;
    public Camera gCam;
    public Animator ani;
    private Quaternion preserve;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        forward = gCam.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        right = new Vector3(forward.z, 0, -forward.x);

        if (controller.isGrounded)
        {
            //  ani.Play("Default Take" , -1, 0f);
            // moveDirection = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).normalized;
            //transform.rotation.SetLookRotation(right);
            moveDirection.x = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).x * speed;
            moveDirection.z = (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right).z * speed;

            //o moveDirection = transform.TransformDirection(moveDirection);
            // transform.rotation = moveDirection;
            //  moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else {
            //moveDirection = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward+ Vector3.up*moveDirection.y).normalized;
            moveDirection.x = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward).x * speed;
            moveDirection.z = (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right).z * speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        transform.rotation.Set(0, 0, 0, 0);
        if ((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S))) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            transform.rotation = Quaternion.Lerp(preserve,Quaternion.LookRotation(Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward), 12f * Time.deltaTime);
        else
            transform.rotation = preserve;

        controller.Move(moveDirection * Time.deltaTime);
        print(transform.rotation + " " + Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));
        preserve = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        //transform.rotation.SetLookRotation((Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward));
    }
}