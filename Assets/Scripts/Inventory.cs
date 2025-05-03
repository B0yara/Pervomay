using System.Collections.Generic;
using UnityEngine;

struct Item
{
    public string itemName;
    public int itemID;
}
public class Inventory
{
    private List<Item> _items;
    public Inventory()
    {
        _items = new List<Item>();
    }


}
