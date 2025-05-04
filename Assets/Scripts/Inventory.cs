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
    GameController _gameController;
    private List<Item> _items;
    public Inventory(GameController gameController)
    {
        _items = new List<Item>();
        _gameController = gameController;
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
            _gameController.newItemPopup.ShowNewItem(item.itemName);
        }
    }
    public void RemoveItem(Item item)
    {
        if (Contains(item))
        {
            _items.Remove(item);
        }
    }
    public void Clear()
    {
        _items.Clear();
    }


}
