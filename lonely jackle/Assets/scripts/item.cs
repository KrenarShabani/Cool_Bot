/*TODO LIST
    - Make size animations a corountine (done)Note: its weird if you click it really fast

*/
using UnityEngine;
using System.Collections;
public class item : MonoBehaviour {
    public int tagSet;
    public GameObject _item;
    private int amount = 10;
    private float posZ = 15f;
    private bool otherflag = false;
    private bool isCrafting = false;
    private RaycastHit hit;

 //   private static Vector3 dest = new Vector3(-1f, -2f, 0f);
    private static Vector3 craftdest = new Vector3(-17f, 25f, 15f);

    public bool getCrafting() { return isCrafting; }
    public int getAmount(){ return amount; }
    public void decrement(){ amount--; }
    public void increment(){ amount++; }
    public void setCrafting(bool t){ isCrafting = t; }
    public void setAmount(int n) { amount = n; }

    void start()
    {

    }
    void Update()
    {
        if (otherflag && !isCrafting)
        {
            var LayerMask = 1 << 8;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 40, Color.cyan);
            if (Physics.Raycast(ray, out hit, 40f, LayerMask))
                 _item.transform.position = new Vector3(hit.point.x, hit.point.y, posZ);
        }
    }
    void OnMouseOver()
    {
        if (!otherflag && !isCrafting)
        {
            StartCoroutine("makeBigger");
            _item.transform.Rotate(0, Time.deltaTime * -100, 0);
        }


        if (Input.GetMouseButtonDown(1) && !isCrafting)
            moveToCrafting();

        else if (Input.GetMouseButtonDown(1) && _item.GetComponent<item>().getCrafting())
        {
            //delete object and send it back to inventory
            GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[_item.GetComponent<item>().tagSet]).GetComponent<item>().increment();
            GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[_item.GetComponent<item>().tagSet]).GetComponentInChildren<text>().setNewNum();
            Destroy(_item);
        }
    }
    void OnMouseExit()
    {
        if (!isCrafting)
        {
            _item.transform.rotation = Quaternion.identity;
            if (!otherflag)
                StartCoroutine("makeSmaller");
                //makeSmaller();
        }
    }
    void OnMouseDown()
    {
        if (!isCrafting)
        {
            otherflag = true;
            _item.transform.rotation = Quaternion.identity;
            //makeSmaller();
            StartCoroutine("makeSmaller");
        }
    }
    void OnMouseUp()
    {
        if (!isCrafting)
        {
            otherflag = false;
            StartCoroutine("makeBigger");
        }
    }
    public IEnumerator makeBigger()
    {
        StopCoroutine("makeSmaller");
        for (float i = _item.transform.localScale.x; i < 1.5f; i += .1f)
        {
            _item.transform.localScale += new Vector3(.1f, .1f, .1f);
            yield return null;
        }

    }
    public IEnumerator makeSmaller()
    {
        StopCoroutine("makeBigger");
        for (float i = _item.transform.localScale.x; i > 1f; i -= .1f)
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
