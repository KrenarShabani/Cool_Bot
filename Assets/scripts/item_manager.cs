/*
    -Make item movement snappier (done)Note : the horizontal edges are shitty (reason is because the camera is in perspective mode 
    and it affects the raycast)

*/

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class item_manager: MonoBehaviour {
    private Component[] items;
    private   float xinitial = 9.2f;
    private   float yinitial = 4f;
    protected float xshift = 2.1f;
    protected float yshift = 2.4f;
    
    private   float x = 9.2f;
    private   float y = 4f;
    private   int cols = 7;
    private   int numerator;
    private string[] crafts;

    void Start ()
    {
        /*
        Hashtable a = new Hashtable();
        a.Add("glass", 1);
        if (a.ContainsKey("glass")) 
        {
            a["glass"] = 2;
        }
        else
            a.Add("glass", 2);
        ICollection ke = a.Keys;
        foreach (string k in ke)
        {
            print(k + " : " + a[k]);
        }*/


        items = GetComponentsInChildren<item>();
        for (int i = 0; i < items.Length; i++)
        {
            reveal(items[i].GetComponent<GameObject>());
            numerator = i;
        }
        if (PlayerPrefs.HasKey("inventory"))
        {
            
            crafts = Utilities.getCrafts();
            if(crafts!=null)
            foreach (string s in crafts)
            {
               // print(s);
                //if (GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)]))
                if(GameObject.Find(s+"(Clone)"))
                {
                   // GameObject newmat = GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)]);
                    GameObject newmat = GameObject.Find(s + "(Clone)");
                   // print(GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)]));
                    newmat.GetComponent<item>().setAmount(newmat.GetComponent<item>().getAmount() + PlayerPrefs.GetInt(s));
                    newmat.GetComponentInChildren<text>().setNewNum();
                }
                else 
                {
                    GameObject inst;
                    //inst = 
                    //inst =  (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/item prefabs/energy crystal", typeof(GameObject));
                    //print(UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)]);
                    //print("Assets/prefabs/item prefabs/" + UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)] + ".prefab");
                  
                    //inst = (GameObject)Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/item prefabs/" + UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)] + ".prefab", typeof(GameObject)));
                    inst = (GameObject)Instantiate((GameObject)Resources.Load(s, typeof(GameObject)));
                    reveal(inst);



                    /*
                    (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/item prefabs/" +
                        UnityEditorInternal.InternalEditorUtility.tags[Int32.Parse(s)], typeof(GameObject));
                       */
                }

            }
        }
    }
    void OnMouseDown()
    {

    }
    void Update()
    {

        if (Input.GetKey(KeyCode.I))
        {
            foreach (Component i in items) 
            {
                //print(i.GetComponent<item>().tagSet);
                PlayerPrefs.SetInt(i.GetComponent<item>().tagSet.ToString(), i.GetComponent<item>().getAmount());
            }
            SceneManager.LoadScene("level");
        }

    }

    void reveal(GameObject item)
    {
        item.tag = "item";
        if (item.GetComponent<Renderer>().enabled == false)
            item.GetComponent<Renderer>().enabled = true;
        item.transform.parent = GameObject.Find("items").transform;
        setPos(item);
        //item.GetComponent<item>().setAmount(PlayerPrefs.GetInt(item.GetComponent<item>().tagSet.ToString()));
        item.GetComponent<item>().setAmount(PlayerPrefs.GetInt(item.name.Substring(0,(item.name.Length - 7))));
        item.GetComponentInChildren<text>().setNewNum();
    }
    public void setPos(GameObject item)
    {
        x = xinitial;
        y = yinitial;
        numerator = 0;
        while (true)
        {
            if (numerator >= cols)
            {
                y -= yshift;
                cols += cols;
                x += xshift * cols;
            }
            x -= xshift;
            numerator++;
            
            item.transform.localPosition = new Vector2(x, y);
            int hitColliders = (Physics.OverlapBox(item.transform.position, new Vector3(1, 1, 1))).Length;
            if (hitColliders < 3)
            {
                item.transform.localPosition = new Vector2(x, y);
                break;
            }
        }
    }
    void setPos(int i)
    {
        //print(i);
        if (i >= cols)
        {
            y -= yshift;
            cols += cols;
            x += xshift * cols;     
        }
        x -= xshift;
        items[i].transform.localPosition = new Vector2(x, y);
    }


    public void unityFam(int[] needed, string[] recipe)
    {
        StartCoroutine(cleanUp(needed, recipe));
    }
    private IEnumerator cleanUp(int[] needed, string[] recipe)
    {
        string name;
        for (int i = 0; i < needed.Length; i++)
        {
            name = recipe[i].Substring(0, recipe[i].Length - 5) + "(Clone)(Clone)";
            print (name);
            print(needed[i]);
           // GameObject[] temp = new GameObject[needed[i]] ;
            PlayerPrefs.SetInt(recipe[i].Substring(0, recipe[i].Length - 5), PlayerPrefs.GetInt(recipe[i].Substring(0, recipe[i].Length - 5)) - needed[i]);

            for (int k = 0; k < needed[i]; k++) 
            {
                Destroy(GameObject.Find(name));   
                yield return null;
            }
          
        }
    }
}
