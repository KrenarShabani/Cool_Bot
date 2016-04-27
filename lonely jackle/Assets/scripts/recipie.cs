using UnityEngine;
using System.Collections;
using System;

public class recipie : MonoBehaviour {
    private GameObject parent;
    private string[] recipe;
    private int[] needed;
    private int[] ingredients;
    private static Vector3 dest = new Vector3(-1f, -2f, 0f);
    private bool flag = true;
    private GameObject createe;
    void start()
    {

    }
    public void updateRecipe(String[] re, int[] ne, GameObject cre)
    {
        recipe = re;
        needed = ne;
        createe = cre;
    }
    public void timeToCraft()
    {
        if (recipe == null & needed == null)
        {
            print("no recipies selected!");
            return;
        }
        ingredients = new int[recipe.Length];
        for (int i = 0; i < recipe.Length; i++)
        {
            ingredients[i] = GameObject.FindGameObjectsWithTag(recipe[i]).Length;
            if (ingredients[i] >= needed[i])
            {
                flag = true;
            }
            else
            {
                flag = false;
                break;
            }

        }
        if (flag)
        {
            print("we can craft!");
        }
        else
        {
            print("we cannot craft!");
        }
        parent = GameObject.FindGameObjectWithTag("itemmanager");
        if (flag)
        {
            if (GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[createe.GetComponent<item>().tagSet]))
            {
                GameObject newmat = GameObject.FindGameObjectWithTag(UnityEditorInternal.InternalEditorUtility.tags[createe.GetComponent<item>().tagSet]);
                newmat.GetComponent<item>().increment();
                newmat.GetComponentInChildren<text>().setNewNum();
            }
            else
            {
                GameObject New = Instantiate(createe);
                New.transform.parent = parent.transform;
                New.transform.localPosition = dest;
                New.GetComponent<item>().setAmount(1);
                New.GetComponentInChildren<text>().setNewNum();
                New.tag = UnityEditorInternal.InternalEditorUtility.tags[New.GetComponent<item>().tagSet];
            }
            parent.GetComponent<item_manager>().fuckunityfam(needed,recipe);
        }
    }


}
