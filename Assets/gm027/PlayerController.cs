using UnityEngine;

namespace GM027
{
    [RequireComponent(typeof(PlayerInventory), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;

        public PlayerInventory Inventory { get; private set; }

        private Rigidbody2D body;
        private Vector2 moveInput;

        private void Awake()
        {
            Inventory = GetComponent<PlayerInventory>();
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(x, y).normalized;
        }

        private void FixedUpdate()
        {
            body.linearVelocity = moveInput * moveSpeed;
        }
    }
}
