/*
    -Make item movement snappier (done)Note : the horizontal edges are shitty

*/

using UnityEngine;
using System.Collections;
using System;

public class item_manager: MonoBehaviour {
    private Component[] items;
    private static float xinitial = 9.2f;
    private static float yinitial = 4f;
    protected static float xshift = 2.1f;
    protected static float yshift = 2.4f;
    
    private static float x = 9.2f;
    private static float y = 4f;
    private static int cols = 7;
    private static int numerator;

    void Start ()
    {
        items = GetComponentsInChildren<item>();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].tag = UnityEditorInternal.InternalEditorUtility.tags[items[i].GetComponent<item>().tagSet];
            if (items[i].GetComponent<Renderer>().enabled == false)
                items[i].GetComponent<Renderer>().enabled = true;
            setPos(i);

            items[i].GetComponentInChildren<text>().setNewNum();
            numerator = i;
        }
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
        if (i >= cols)
        {
            y -= yshift;
            cols += cols;
            x += xshift * cols;     
        }
        x -= xshift;
        items[i].transform.localPosition = new Vector2(x, y);
    }

    public void fuckunityfam(int[] needed, string[] recipe)
    {
        StartCoroutine(cleanUp(needed, recipe));
    }
    private IEnumerator cleanUp(int[] needed, string[] recipe)
    {
        for (int i = 0; i < needed.Length; i++)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(recipe[i]);
            for (int j = 0; j < needed[i]; j++)
            {
                Destroy(temp[j]);
            }

            yield return null;
        }
    }
}
