using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private readonly Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();

    public int Count(ItemType type)
    {
        return items.TryGetValue(type, out int count) ? count : 0;
    }

    public void Add(ItemType type, int amount = 1)
    {
        items[type] = Count(type) + amount;
        Debug.Log(ItemTypeName.Get(type) + " 획득! 현재 개수: " + Count(type));
    }

    public bool TryUse(ItemType type)
    {
        if (Count(type) <= 0)
            return false;

        items[type]--;
        return true;
    }
}
