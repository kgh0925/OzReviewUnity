using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private readonly Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();
    private readonly Dictionary<string, int> Inventory = new Dictionary<string, int>();
    private readonly List<string> Inven = new List<string>();

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

    public int GetItemCount(string itemId)
    {
        return Inventory.TryGetValue(itemId, out int count) ? count : 0;
    }

    public void AddItem(string itemId)
    {
        Inven.Add(itemId);
        if(Inventory.TryGetValue(itemId, out int count))
        {
            Inventory[itemId]++;
        }
        else
        {
            Inventory.Add(itemId, 1);
        }

    }

    public void RemoveItem(string itemId, int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning("삭제할 수량은 1 이상이어야 합니다.");
            return;
        }

        if (!Inventory.TryGetValue(itemId, out int currentCount))
        {
            Debug.LogWarning($"{itemId} 아이템이 없습니다.");
            return;
        }

        if(currentCount < count)
        {
            Debug.LogWarning($"보유 수량보다 더 많은 삭제 요청이 들어왔습니다");
            return;
        }
        int tempCount = count;
        int remainingCount = currentCount - tempCount;

        if (remainingCount > 0)
        {
            Inventory[itemId] = remainingCount;
        }
        else
        {
            Inventory.Remove(itemId);
        }

        for (int i = Inven.Count - 1; i >= 0 && tempCount > 0; i--)
        {
            if (Inven[i] == itemId)
            {
                Inven.RemoveAt(i);
                tempCount--;
            }
        }
    }

    public void InventoryList()
    {
        foreach(KeyValuePair<string, int> kvp in Inventory)
        {
            Debug.Log($"{kvp.Key}의 수량 : {kvp.Value}");
        }
    }
}
