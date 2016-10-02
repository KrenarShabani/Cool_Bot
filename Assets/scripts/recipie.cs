/*
- Make better "ingredients needed: " display

*/

using UnityEngine;
using System.Collections;
using System;

public class recipie : MonoBehaviour
{
    private GameObject parent;
    private GameObject createe;
    public TextMesh requirements;
    private string[] recipe;
    private int[] needed;
    private int[] ingredients;
    private bool flag = true;
    private Vector3 loc = new Vector3(5.3f, 6, 0);

    void start()
    {
        requirements.characterSize = 10;
        // requirements.fontSize = 30;
        //requirements.text = ("Select a recipe!");
    }
    public void updateRecipe(String[] re, int[] ne, GameObject cre)
    {

        GameObject[] old = GameObject.FindGameObjectsWithTag("display");
        for (int i = 0; i < old.Length; i++)
        {
            Destroy(old[i]);
        }

        recipe = re;
        needed = ne;
        createe = cre;


        string display = "\t";
        //display = ("you need:\t");
        GameObject temp;
        parent = GameObject.FindGameObjectWithTag("itemmanager");
        float x = 0;
        for (int i = 0; i < recipe.Length; i++)
        {
            //temp = (GameObject)Instantiate(GameObject.FindGameObjectWithTag(recipe[i].Substring(0, recipe[i].Length - 5)), loc, Quaternion.identity);
            print("Assets/prefabs/item prefabs/" + recipe[i].Substring(0, recipe[i].Length - 5) + ".prefab"); 
            temp = (GameObject)Instantiate((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/item prefabs/" + recipe[i].Substring(0, recipe[i].Length - 5) + ".prefab", typeof(GameObject)));

            if (temp.GetComponent<Renderer>().enabled == false)
                temp.GetComponent<Renderer>().enabled = true;
            
            temp.transform.parent = parent.transform;
            temp.transform.localPosition = new Vector3(loc.x + x, loc.y);
            temp.GetComponentInChildren<text>().setNull();
            temp.GetComponent<Rigidbody>().detectCollisions = false;
            temp.tag = ("display");
            x -= 3.5f;
            display += (" x" + needed[i] + " +\t ");
        }
        display = display.Substring(0, display.Length - 3);
        display += '=';
        temp = (GameObject)Instantiate(createe, loc, Quaternion.identity);

        if (temp.GetComponent<Renderer>().enabled == false)
            temp.GetComponent<Renderer>().enabled = true;

        temp.transform.parent = parent.transform;
        temp.transform.localPosition = new Vector3(loc.x + x, loc.y);
        temp.GetComponentInChildren<text>().setNull();
        temp.GetComponent<Rigidbody>().detectCollisions = false;
        temp.tag = ("display");
        x -= 3.5f;
        print(display);
        requirements.text = display;
    }
    public void timeToCraft()
    {
        if (recipe == null & needed == null)
        {
            print("no recipies selected! (or you didnt calibrate the recipe correctly?)");
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
                New.transform.parent = GameObject.Find("items").transform;
                parent.GetComponent<item_manager>().setPos(New);
                New.GetComponent<item>().setAmount(1);
                New.GetComponentInChildren<text>().setNewNum();
                New.tag = UnityEditorInternal.InternalEditorUtility.tags[New.GetComponent<item>().tagSet];

                if (New.GetComponent<Renderer>().enabled == false)
                    New.GetComponent<Renderer>().enabled = true;

            }
            charinventory.addItem(createe.GetComponent<item>().tagSet);
            
            parent.GetComponent<item_manager>().unityFam(needed, recipe);
        }
    }
}
