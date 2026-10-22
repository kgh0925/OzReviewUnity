using UnityEngine;

namespace GM034
{
    public class GM034PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
