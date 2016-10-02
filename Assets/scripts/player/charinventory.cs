using UnityEngine;
using System.Collections;

public class charinventory : MonoBehaviour {
    private static string INVENTORY;
    private static string _inventory;
    static int[] items;
    static int size = 0;
	// Use this for initialization
	void Start () {
        _inventory = "inventory";
        items = new int[10];
        
        //print(PlayerPrefs.GetInt("9"));
        //PlayerPrefs.DeleteAll();        
        if (PlayerPrefs.GetString(_inventory) != null)
            INVENTORY = PlayerPrefs.GetString(_inventory);
           // INVENTORY = "13,11,9";
       // PlayerPrefs.SetInt("15", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void addItem(int tag)
    {

        items[size] = tag;
        // print(items[size]);
        size++;

        if (PlayerPrefs.GetInt(tag.ToString()) != 0)
        {
            PlayerPrefs.SetInt(tag.ToString(), PlayerPrefs.GetInt(tag.ToString()) + 1);
        }
        else
        {

            PlayerPrefs.SetInt(tag.ToString(), 1);
            if (INVENTORY == null)
            {
                INVENTORY = tag.ToString();
                //print(INVENTORY);
            }
            else
            {
                INVENTORY += "," + tag.ToString();
                //print(INVENTORY);            
            }
            PlayerPrefs.SetString(_inventory, INVENTORY);
        }
        //PlayerPrefs.SetString(_inventory, INVENTORY);
        print(PlayerPrefs.GetString(_inventory));
        print(PlayerPrefs.GetInt(tag.ToString()));
    }

    public static bool isFull()
    {
        if (size == items.Length)
            return true;
        else return false;
    }
}
