/*TODO LIST
    - Add list of craftable options
    - Make Crafting recipies through the scene
*/

using UnityEngine;
using System.Collections;

public class crafting : MonoBehaviour {
    public GameObject _item;
    private static Vector3 dest = new Vector3(-1f, -2f, 0f);
    public GameObject parent;
	// Use this for initialization
	void Start () {
       // print(items.Length);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void timeToCraft()
    {
        int r = GameObject.FindGameObjectsWithTag("rockcraft").Length;
        int m = GameObject.FindGameObjectsWithTag("metalcraft").Length;
        if (m >= 1 && r >= 3)
        {
            if (GameObject.FindGameObjectWithTag("new"))
            {
                GameObject newmat = GameObject.FindGameObjectWithTag("new");
                newmat.GetComponent<item>().increment();
                newmat.GetComponentInChildren<text>().setNewNum();
            }
            else
            {
                GameObject New = Instantiate(Resources.Load("new", typeof(GameObject)) as GameObject);
                //GameObject NEW = (GameObject)Instantiate(_item, _item.transform.localPosition, Quaternion.identity);
                New.transform.parent = parent.transform;
                New.transform.localPosition = dest;
                New.GetComponent<item>().setAmount(1);
                New.GetComponentInChildren<text>().setNewNum();
                New.tag = UnityEditorInternal.InternalEditorUtility.tags[New.GetComponent<item>().tagSet];
            }
            //items[metal] -= 1;
            //items[rock] -= 3;

            GameObject Items;
            GameObject[] rocks;
            Items = GameObject.FindGameObjectWithTag("metalcraft");
            rocks = GameObject.FindGameObjectsWithTag("rockcraft");
            for (int i = 0; i < 3; i++)
            {
                Destroy(rocks[i]);
            }
            Destroy(Items);
        }
        else
        {
            print("not enough materials!");
        }
    }
}
