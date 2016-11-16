using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class lockOnAssists : MonoBehaviour {
    List<GameObject> enemies = new List<GameObject>();
    private static RaycastHit hit;
    public GameObject cam;
    private GameObject Fenemy;
    public LayerMask NonIgnore;
    public GameObject canvas;
    public RectTransform retical;
	// Use this for initialization
	void Start () {
        canvas.GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        var ray = Camera.main.ScreenPointToRay(cam.transform.position);
        //var ray = Camera.main.ScreenPointToRay(GameObject.Find("Jackle").transform.position);
        //ray.origin = cam.transform.position;

        ray.direction = (GameObject.Find("Jackle").transform.position - cam.transform.position) + 5f * Vector3.up;
       // ray.direction = (GameObject.Find("Jackle").transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 55f, Color.red);
        if (Physics.Raycast(ray, out hit, 55f, NonIgnore) &&
            hit.collider != null && hit.collider.tag == "enemy" && 
            enemies.Contains(hit.collider.GetComponentInParent<robotenemy>().gameObject))
        {
        //print(hit.collider.name);

            //print("locked on!");
            Fenemy = hit.collider.GetComponentInParent<robotenemy>().gameObject;
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            Vector2 ViewportPosition = cam.GetComponent<Camera>().WorldToViewportPoint(Fenemy.transform.GetChild(0).position);
            Vector2 WorldObject_ScreenPosition = new Vector2(((ViewportPosition.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x * .5f)),((ViewportPosition.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*.5f)));
            
            //retical.transform.position = Fenemy.transform.position + 5f * Vector3.up;

            canvas.GetComponent<Canvas>().enabled = true;
            retical.anchoredPosition = WorldObject_ScreenPosition;

            //print(hit.collider.name);
        
        }
        else
        {
            //if(hit.collider != null)   print(hit.collider.name);
            canvas.GetComponent<Canvas>().enabled = false;
            //print("not locked on!");
            Fenemy = null;
        }
	}
    void OnTriggerEnter(Collider other) 
    {
        //print(other.tag);
        //print(other.name);
        if (other.tag == "enemy" ) 
        {
            //print (other.GetComponentInChildren)
            if (!enemies.Contains(other.gameObject) && !other.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dead"))
            {
                //print("added to the list! " + other.name);

                enemies.Add(other.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "enemy" && enemies.Contains(other.gameObject)) 
        {
            enemies.Remove(other.gameObject);
            //print("removed from the list!" + other.name);
        }
    }
    public void hasDied(GameObject deadguy) 
    {
        if (enemies.Contains(deadguy)) 
        {
            //print(deadguy.name + " has died and has been removed from the list!");
            enemies.Remove(deadguy);
        }
    }
    public bool isLocked() 
    {
        return Fenemy != null ? true : false;
    }
    public Transform getLockOn() 
    {
        return Fenemy.transform;
    }
}
