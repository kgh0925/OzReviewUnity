using UnityEngine;
using UnityEngine.UI;

public class GM031InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text[] hudTexts;
    [SerializeField] private Text[] slotTexts;

    private readonly string[] itemIds = { "potion", "gold", "key_red" };

    private void Start()
    {
        Add("potion", 3);
        Add("gold", 120);
        Add("key_red", 1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Add("potion", 3);
        }
        for (int i = 0; i < itemIds.Length; i++)
        {
            int count = inventory.GetItemCount(itemIds[i]);
            hudTexts[i].text = itemIds[i] + " : " + count;
            slotTexts[i].text = itemIds[i] + "\nx" + count;
        }
    }

    public void Open() => inventoryPanel.SetActive(true);
    public void Close() => inventoryPanel.SetActive(false);

    private void Add(string id, int count)
    {
        for (int i = 0; i < count; i++)
            inventory.AddItem(id);
    }
}
