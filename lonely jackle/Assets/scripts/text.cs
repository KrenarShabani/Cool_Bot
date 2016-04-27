/*
    -Change font
    -Stop making it rotate
*/

using UnityEngine;
using System.Collections;

public class text : MonoBehaviour {
    public GameObject _item;
    public TextMesh amount;
    private int quantity;

    void start()
    {
        if (!_item.GetComponent<item>().getCrafting())
        {
            quantity = _item.GetComponent<item>().getAmount();
            amount.text = quantity.ToString();
        }
        else
            amount.text = null;
    }
    void Update () {

    }

    public void setNewNum()
    {
        if (!_item.GetComponent<item>().getCrafting())
        {
            quantity = _item.GetComponent<item>().getAmount();
            amount.text = quantity.ToString();
        }
        else
            amount.text = null;
    }
}
