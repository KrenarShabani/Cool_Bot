using UnityEngine;
using System.Collections;

public class movecam : MonoBehaviour {
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform follow;
    private Vector3 targetPosition;
    // Use this for initialization
    public bool orbitY;
	void Start () {
        follow = GameObject.FindWithTag("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void LateUpdate()
    {
       targetPosition = follow.position;
      //  targetPosition =  Vector3.forward * -distanceAway;
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x-distanceAway,transform.position.y, targetPosition.z-distanceAway), Time.deltaTime * smooth);
        if(orbitY)
        {
            transform.RotateAround(follow.transform.position, Vector3.up, Time.deltaTime * 15);
        }
       transform.LookAt(follow);
    
    }
}
