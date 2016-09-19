/*TODO LIST
    - Make size animations a corountine (done)Note: its weird if you click it really fast

*/
using UnityEngine;
using System.Collections;
using System;

public class item : MonoBehaviour
{
    public int tagSet;
    public GameObject _item;
    private int amount = 10;
    private bool otherflag = false;
    private bool isCrafting = false;
    private bool isMouseOver = false;
    private static bool isOnMouseDown = false;
    private static RaycastHit hit;
    private static float shiftx = 2.1f;
    public float maxSize;
    public float minSize;

    //   private static Vector3 dest = new Vector3(-1f, -2f, 0f);
    private static Vector3 craftdest = new Vector3(-17f, 25f, 15f);

    public bool getCrafting() { return isCrafting; }
    public int getAmount() { return amount; }
    public void decrement() { amount--; }
    public void increment() { amount++; }
    public void setCrafting(bool t) { isCrafting = t; }
    public void setAmount(int n) { amount = n; }
    private Vector3 te = new Vector3(0, 180, 0);
    void Start()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        if (otherflag & !isCrafting)
        {

            var LayerMask = 1 << 8;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 74f, Color.cyan);
            if (Physics.Raycast(ray, out hit, 74f, LayerMask))
            {
                int hitColliders = (Physics.OverlapBox(new Vector3(hit.point.x, hit.point.y, hit.point.z), new Vector3(1, 1, 1))).Length;

                if (hitColliders < 2)
                    _item.transform.position = new Vector3(shiftx * (float)(Math.Round((hit.point.x / shiftx))), 2.4f * (float)(Math.Round((hit.point.y / 2.4))), hit.point.z);
            }
        }
    }
    void OnMouseOver()
    {
        isMouseOver = true;
        if (!otherflag && !isCrafting && !isOnMouseDown)
        {
            StartCoroutine("makeBigger");

            _item.GetComponentInChildren<text>().amount.transform.eulerAngles = te;
            _item.transform.Rotate(0, Time.deltaTime * -100, 0);
        }




        // this is for when item is in crafting box
        if (Input.GetMouseButtonDown(1) && !isCrafting)
        {
            /* _item.transform.localScale -= new Vector3(1f, 1f, 1f);
             StartCoroutine("makeBigger");*/
            moveToCrafting();
        }

        else if (Input.GetMouseButtonDown(1) && isCrafting)
        {
            //delete object and send it back to inventory
            GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[_item.GetComponent<item>().tagSet]).GetComponent<item>().increment();
            GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[_item.GetComponent<item>().tagSet]).GetComponentInChildren<text>().setNewNum();
            Destroy(_item);
        }
    }
    void OnMouseExit()
    {
        isMouseOver = false;
        if (!isCrafting)
        {
            _item.transform.rotation = Quaternion.identity;
            _item.GetComponentInChildren<text>().amount.transform.eulerAngles = te;
            if (!otherflag)
                StartCoroutine("makeSmaller");
        }
    }
    void OnMouseDown()
    {
        if (!isCrafting)
        {

            isOnMouseDown = true;
            otherflag = true;
            _item.transform.rotation = Quaternion.identity;
            _item.GetComponentInChildren<text>().amount.transform.eulerAngles = te;
            StartCoroutine("makeSmaller");
        }
    }
    void OnMouseUp()
    {
        isOnMouseDown = false;
        if (!isMouseOver)
        {
            otherflag = false;
        }

        if (isMouseOver & !isCrafting)
        {
            StartCoroutine("makeBigger");
            otherflag = false;
        }
    }
    public IEnumerator makeBigger()
    {
        StopCoroutine("makeSmaller");
        for (float i = _item.transform.localScale.x; i < maxSize; i += .1f)
        {
            _item.transform.localScale += new Vector3(.1f, .1f, .1f);
            yield return null;
        }

    }
    public IEnumerator makeSmaller()
    {
        StopCoroutine("makeBigger");
        for (float i = _item.transform.localScale.x; i > minSize; i -= .1f)
        {
            _item.transform.localScale += new Vector3(-.1f, -.1f, -.1f);
            yield return null;
        }
    }
    void moveToCrafting()
    {
        if (amount > 0)
        {
            GameObject craft = (GameObject)Instantiate(_item, craftdest, Quaternion.identity);
            craft.GetComponent<Rigidbody>().isKinematic = false;
            craft.GetComponent<Rigidbody>().useGravity = true;

            craft.GetComponent<item>().StartCoroutine("makeSmaller");
            craft.GetComponent<item>().setCrafting(true);
            craft.GetComponentInChildren<text>().setNewNum();
            amount--;
            _item.GetComponentInChildren<text>().setNewNum();

            craft.tag = UnityEditorInternal.InternalEditorUtility.tags[craft.GetComponent<item>().tagSet + 1];
        }
    }
}
