using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    public void Configure(ItemType type)
    {
        itemType = type;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null)
            return;

        inventory.Add(itemType);
        Destroy(gameObject);
    }
}
