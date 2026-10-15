using UnityEngine;

namespace GM027
{
    public class Generator : MonoBehaviour, IInteractable
    {
        [SerializeField] private int multiplier = 1;
        [SerializeField] private MainCore mainCore;

        public bool Active { get; private set; }
        public int Multiplier => multiplier;
        public string InteractionText => Active ? "이미 작동 중인 발전기" : "E : 코일 장착 (x" + multiplier + ")";

        private SpriteRenderer objectRenderer;
        private PlayerController playerInRange;

        private void Awake()
        {
            objectRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact(PlayerController player)
        {
            if (Active || mainCore.IsFinished) return;

            EnergyCoil coil = player.Inventory.UseCoil();
            if (coil == null) return;

            Active = true;
            objectRenderer.material.color = new Color(0.25f, 1f, 0.45f);
            mainCore.ReceivePower(coil.powerValue * multiplier);
        }

        private void Update()
        {
            if (playerInRange != null && Input.GetKeyDown(KeyCode.E))
                Interact(playerInRange);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                playerInRange = player;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == playerInRange)
                playerInRange = null;
        }

    }
}
