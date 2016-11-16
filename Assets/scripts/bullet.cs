using UnityEngine;
using System.Collections;

public class bullet: MonoBehaviour
{
    private Component lockonsys;
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;

    private bool LockedON = false;
    private Transform enemylocked;

    bool RPM = true;

    public GameObject Bomb;

    private GameObject PTRbullet;

    // Use this for initialization
    void Start()
    {
        PTRbullet = Bullet;
        lockonsys = GameObject.Find("Jackle").GetComponentInChildren<lockOnAssists>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.E) || Input.GetMouseButton(1)) && RPM )
        {
            GameObject.Find("Jackle").GetComponent<controler>().ani.SetBool("ratatat", true);

            if (lockonsys.GetComponent<lockOnAssists>().isLocked())
            {
                LockedON = true;
                enemylocked = lockonsys.GetComponent<lockOnAssists>().getLockOn();
            }
            else
            {
                LockedON = false;
                enemylocked = null;
            }

            //The Bullet instantiation happens here.

            GameObject temp = Instantiate(PTRbullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            temp.GetComponent<missle>().setValues(LockedON, enemylocked);

            RPM = false;
            StartCoroutine(reset(.2f));

        }
        else 
        {
            //GameObject.Find("Jackle").GetComponent<controler>().ani.SetBool("ratatat", false);
        }
        if ((!Input.GetKey(KeyCode.E) && !Input.GetMouseButton(1))) 
        {
            GameObject.Find("Jackle").GetComponent<controler>().ani.SetBool("ratatat", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (PTRbullet.name == Bullet.name)
            {
                PTRbullet = Bomb;
            }
            else 
            {
                PTRbullet = Bullet;
            }
        }
    }

    IEnumerator reset(float time) 
    {
        yield return new WaitForSeconds(time);
        RPM = true;
    }



}