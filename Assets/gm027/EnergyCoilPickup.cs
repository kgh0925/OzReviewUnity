using UnityEngine;

namespace GM027
{
    public class EnergyCoilPickup : MonoBehaviour
    {
        [SerializeField] private int powerValue;
        public int PowerValue => powerValue;

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory == null) return;

            inventory.Add(new EnergyCoil(powerValue));
            Destroy(gameObject);
        }

    }
}
