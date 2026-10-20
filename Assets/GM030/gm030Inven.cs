using System.Collections.Generic;
using UnityEngine;

public class gm030Inven : MonoBehaviour
{
    public Dictionary<string, int> itemPrices = new Dictionary<string, int>();

    [Header("Crafting Test")]
    [SerializeField] private PlayerInventory testInventory;

    void Start()
    {
        if(!itemPrices.ContainsKey("potion"))
        {
            itemPrices.Add("potion", 50);
        }
        if (!itemPrices.ContainsKey("sword"))
        {
            itemPrices.Add("sword", 300);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ItemList();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            testInventory.InventoryList();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GiveTestMaterials();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TryCraftTestAxe();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TryCraftMagicSword();
        }
    }

    public int GetPrice(string itemId)
    {
        if(itemPrices.TryGetValue(itemId, out int price))
        {
            return price;
        }
        else
        {
            Debug.Log("없는 아이템 ID");
            return 0;
        }
    }

    public void ItemList()
    {
        foreach (KeyValuePair<string, int> item in itemPrices)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }

    public bool Crafting(string recipeId, Dictionary<string,int> itemList, PlayerInventory inventory)
    {
        foreach(KeyValuePair<string, int> item in itemList)
        {
            if(inventory.GetItemCount(item.Key) < item.Value)
            {
                Debug.Log("수량이 부족하여 아이템을 제작할 수 없습니다.");
                return false;
            }
        }

        foreach (KeyValuePair<string, int> item in itemList)
        {
            inventory.RemoveItem(item.Key, item.Value);
        }
        inventory.AddItem(recipeId);

        Debug.Log("아이템 제작 완료");
        return true;
    }

    /// <summary>
    /// 테스트 코드 ( ai 제작 )
    /// </summary>
    [ContextMenu("Test - Give Materials")]
    public void GiveTestMaterials()
    {
        if (!TryGetTestInventory(out PlayerInventory inventory))
        {
            return;
        }

        inventory.AddItem("test_wood");
        inventory.AddItem("test_wood");
        inventory.AddItem("test_stone");

        Debug.Log($"[A: 재료 지급] 나무: {inventory.GetItemCount("test_wood")}, " +
                  $"돌: {inventory.GetItemCount("test_stone")}");
    }

    [ContextMenu("Test - Craft Axe")]
    public void TryCraftTestAxe()
    {
        if (!TryGetTestInventory(out PlayerInventory inventory))
        {
            return;
        }

        Dictionary<string, int> recipe = new Dictionary<string, int>
        {
            { "test_wood", 2 },
            { "test_stone", 1 }
        };

        bool crafted = Crafting("test_axe", recipe, inventory);

        Debug.Log($"[S: 도끼 제작] 결과: {(crafted ? "성공" : "실패")}, " +
                  $"도끼: {inventory.GetItemCount("test_axe")}, " +
                  $"나무: {inventory.GetItemCount("test_wood")}, " +
                  $"돌: {inventory.GetItemCount("test_stone")}");
    }

    [ContextMenu("Test - Craft Magic Sword")]
    public void TryCraftMagicSword()
    {
        if (!TryGetTestInventory(out PlayerInventory inventory))
        {
            return;
        }

        Dictionary<string, int> recipe = new Dictionary<string, int>
        {
            { "test_missing_crystal", 1 }
        };

        bool crafted = Crafting("test_magic_sword", recipe, inventory);

        Debug.Log($"[D: 마법검 제작] 결과: {(crafted ? "성공" : "실패")}, " +
                  $"마법검: {inventory.GetItemCount("test_magic_sword")}, " +
                  $"크리스탈: {inventory.GetItemCount("test_missing_crystal")}");
    }

    private bool TryGetTestInventory(out PlayerInventory inventory)
    {
        inventory = testInventory;

        if (inventory == null)
        {
            inventory = GetComponent<PlayerInventory>();
        }

        if (inventory == null)
        {
            Debug.LogError("Crafting 테스트용 PlayerInventory가 없습니다. " +
                           "Inspector의 Test Inventory에 PlayerInventory를 연결하세요.");
            return false;
        }

        return true;
    }
}
