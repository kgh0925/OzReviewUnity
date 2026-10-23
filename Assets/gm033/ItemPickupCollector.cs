using UnityEngine;

namespace GM033
{
    [RequireComponent(typeof(PlayerInventory))]
    public class ItemPickupCollector : MonoBehaviour
    {
        private PlayerInventory inventory;

        private void Awake()
        {
            inventory = GetComponent<PlayerInventory>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            WorldItem worldItem = other.GetComponent<WorldItem>();

            if (worldItem != null)
                worldItem.TryCollect(inventory);
        }
    }
}
