/*
    -Make item movement snappier 

*/

using UnityEngine;
using System.Collections;
using System;

public class item_manager: MonoBehaviour {
    private Component[] items;
    private float x = 8;
    private float y = 4;
    private int cols = 7;
    private int j = 1;
    // Use this for initialization
    void Start ()
    {
        items = GetComponentsInChildren<item>();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].tag = UnityEditorInternal.InternalEditorUtility.tags[items[i].GetComponent<item>().tagSet];
            if (items[i].GetComponent<Renderer>().enabled == false)
                items[i].GetComponent<Renderer>().enabled = true;
            setPos(i);
            items[i].transform.localPosition = new Vector2(x, y);
            items[i].GetComponentInChildren<text>().setNewNum();
        }
    }

    void setPos(int i)
    {
        if (i >= cols)
        {
            y -= 2.4f;
            cols += 7;
            x += 2.1f * 7f;
            j++;
        }
        x -= 2.1f;
    }
	// Update is called once per frame
	void Update () {
	
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
