using UnityEngine;

namespace GM028
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        public Vector2 FacingDirection { get; private set; } = Vector2.right;

        private Rigidbody2D body;
        private Vector2 moveInput;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            moveInput = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;

            if (moveInput != Vector2.zero)
                FacingDirection = moveInput;
        }

        private void FixedUpdate()
        {
            body.linearVelocity = moveInput * moveSpeed;
        }
    }
}
