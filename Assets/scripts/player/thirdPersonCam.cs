using UnityEngine;
using System.Collections;

public class thirdPersonCam : MonoBehaviour {


    public Transform lookAt;
    public Transform camTransform;

    private const float Y_ANGLE_MIN = -40f;
    private const float Y_ANGLE_MAX = 40f;

    private const float MAX_ZOOM = 2.5f;
    private const float MIN_ZOOM = 15f;

    private Camera cam;
    float dis = 8f;
    Vector3 dest;
    private float distance = 15f;
    private float currentX = 0;
    private float currentY = 0f;
    public float sensivityX = 4f;
    public float sensivityY = 1f;



    private void Start()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY * sensivityY, currentX * sensivityX, 0);


        camTransform = transform;
        cam = Camera.main;

    }
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        dis += Input.GetAxis("Mouse ScrollWheel");
        dis = Mathf.Clamp(dis, MAX_ZOOM, MIN_ZOOM);
        //print(dis);
    }


    private void LateUpdate()
    {
        RaycastHit hit;

        Vector3 dir = new Vector3(0, 0, - dis);
        //dir += -Vector3.forward;
        Quaternion rotation = Quaternion.Euler(currentY * sensivityY, currentX * sensivityX, 0);
        dest = lookAt.position + rotation * dir;

        if (Physics.Linecast(camTransform.position, dest, out hit))
        {
            camTransform.position = Vector3.Lerp(camTransform.position, hit.point +hit.normal* 0.5f, 20f * Time.deltaTime);
        }else
            camTransform.position = Vector3.Lerp(camTransform.position, dest, Time.deltaTime * 20f);

        if(currentY>= lookAt.position.y)
            camTransform.LookAt(lookAt.position+Vector3.up*(Mathf.Sqrt(currentY/lookAt.position.y))/2);
        else
            camTransform.LookAt(lookAt.position + 3f*Vector3.up);
       // print(currentY);
    }


}

