using UnityEngine;
using System.Collections;

public class anotherCAMFAM : MonoBehaviour {



    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 3.4f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -8;
        public float zoomSmooth = 10;
        public float maxZoom = -2;
        public float minZoom = -15;
    }
    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }
    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HOTIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }
    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    public Transform target;
    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    CharacterController charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;
    
	// Use this for initialization
	void Start () {
        SetCameraTarget(target);

        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
    }

    void SetCameraTarget(Transform t)
    {
        target = t;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        MoveToTarget();
        LookAtTarget();
	}
    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HOTIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void Update() 
    {
        GetInput();
        OribitTarget();
        ZoomInOnTarget();
    }
    void MoveToTarget()
    {
        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
    }
    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
    }
    void OribitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
        }
        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;

        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }
        if (orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }
    }
    void ZoomInOnTarget()
    {

    }
}
