using UnityEngine;

namespace GM033
{
    [RequireComponent(typeof(Collider2D))]
    public class WorldItem : MonoBehaviour
    {
        [SerializeField] private string itemId;

        public string ItemId => itemId;

        public bool TryCollect(PlayerInventory inventory)
        {
            if (inventory == null || !ItemCatalogManager.IsRegistered(itemId))
                return false;

            inventory.AddItem(itemId);
            Destroy(gameObject);
            return true;
        }
    }
}
