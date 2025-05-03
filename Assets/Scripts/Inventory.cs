using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Item
{
    public string itemName;
    public int itemID;
    public override bool Equals(object obj)
    {
        if (obj is Item item)
        {
            return itemID == item.itemID;
        }
        return false;
    }
}
public class Inventory
{
    private List<Item> _items;
    public Inventory()
    {
        _items = new List<Item>();
    }

    public bool Contains(Item item)
    {
        return _items.Contains(item);
    }

    public void AddItem(Item item)
    {
        if (!Contains(item))
        {
            _items.Add(item);
        }
    }
    public void RemoveItem(Item item)
    {
        if (Contains(item))
        {
            _items.Remove(item);
        }
    }


}
