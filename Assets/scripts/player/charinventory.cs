using UnityEngine;
using System.Collections;

public class charinventory : MonoBehaviour {
    private string inventory;
    int[] items;
    int size = 0;
	// Use this for initialization
	void Start () {
        items = new int[10];
        PlayerPrefs.DeleteAll();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addItem(int tag)
    {
        items[size] = tag;
        size++;
       // print(items[size-1]);
        if (PlayerPrefs.HasKey(tag.ToString()))
        {
            PlayerPrefs.SetInt(tag.ToString(), PlayerPrefs.GetInt(tag.ToString()) + 1);
        }
        else
        {

            PlayerPrefs.SetInt(tag.ToString(), 1);
            if (inventory == null)
            {
                inventory = tag.ToString();
            }
            else
                inventory += "," + tag.ToString();
        }
        PlayerPrefs.SetString("inventory", inventory);
        //print(PlayerPrefs.GetString("inventory"));
        //print(PlayerPrefs.GetInt(tag.ToString()));
    }

    public bool isFull()
    {
        if (size == items.Length)
            return true;
        else return false;
    }
}
