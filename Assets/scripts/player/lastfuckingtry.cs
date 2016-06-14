using UnityEngine;
using System.Collections;

public class lastfuckingtry : MonoBehaviour
{

    public Transform lookAt;
    public Transform camTransform;

    private const float Y_ANGLE_MIN = -40f;
    private const float Y_ANGLE_MAX = 40f;

    public bool smoothFloow = true;
    public float smooth = 0.05f;

    public float adjustmentDistance = -7;
    Vector3 targetPosOffset = new Vector3(0, 3.4f, 0);
    private const float MAX_ZOOM = 2.5f;
    private const float MIN_ZOOM = 15f;

    float dis = 8f;
    Vector3 dest;
    private float distance = 15f;
    private float currentX = 0;
    private float currentY = 0f;
    public float sensivityX = 4f;
    public float sensivityY = 1f;

    public DebugSettings debug = new DebugSettings();
    public CollisionHandeler collision = new CollisionHandeler();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero;
    Vector3 camVel = Vector3.zero;


    private void Start()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY * sensivityY, currentX * sensivityX, 0);

        collision.Initialize(Camera.main);

        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.adjustedCameraClipPoints);
        camTransform = transform;

    

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

        Vector3 dir = new Vector3(0, 0, -dis);
        //dir += -Vector3.forward;
        
        Quaternion rotation = Quaternion.Euler(currentY * sensivityY, currentX * sensivityX, 0);
        dest = lookAt.position + rotation * dir;
        destination = lookAt.position + rotation * dir;
        if (collision.colliding)
        {
            adjustedDestination = Quaternion.Euler(rotation.x,rotation.y,0) *-Vector3.forward * adjustmentDistance ;
            adjustedDestination += targetPos;

            if (smoothFloow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination + rotation* Vector3.forward, ref camVel, smooth);
            }
            else
                transform.position = adjustedDestination;
        }
        else
        {
            if (smoothFloow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, destination+ rotation * dir, ref camVel, smooth);
            }
            else
                transform.position = adjustedDestination;
        }

        //camTransform.position = Vector3.Lerp(camTransform.position, dest, Time.deltaTime * 10f);

        camTransform.LookAt(lookAt.position);
        // print(currentY);
    }

    private void FixedUpdate()
    {
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.adjustedCameraClipPoints);

        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debug.drawDesiredCollisionLines)
            {
                Debug.DrawLine(transform.position, collision.desiredCameraClipPoints[i], Color.white);
            }
            if (debug.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(transform.position, collision.adjustedCameraClipPoints[i], Color.red);
            }
        }

        collision.CheckColliding(targetPos);
        adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(lookAt.position);
    }

    [System.Serializable]
    public class CollisionHandeler
    {
        public LayerMask collisionLayer;

        public bool colliding = false;
        public Vector3[] adjustedCameraClipPoints;
        public Vector3[] desiredCameraClipPoints;
        Camera camera;
        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;

            //clear contents of intoArray
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f)*z;
            float y = x / camera.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; // added and rotated the point relative to camera
             //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;
            //camera's position
            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 FromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(FromPosition, clipPoints[i] - FromPosition);
                float distance = Vector3.Distance(clipPoints[i],FromPosition);
                if (Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }
            return false;
        }


        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }


            if (distance == -1)
                return 0;
            else return distance;
        }
        public void CheckColliding(Vector3 targetPosition)
        {
            if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
                colliding = false;
        }
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }
}