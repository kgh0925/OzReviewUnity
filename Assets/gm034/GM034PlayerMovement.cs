using GM033;
using UnityEngine;

namespace GM034
{
    public class GM034PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float atkCol = 2f;
        private float lastAtkCol = -999f;
        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, vertical, 0f).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            HealthPointManager hp = collision.GetComponent<HealthPointManager>();

            if (hp != null && (lastAtkCol + atkCol < Time.time))
            {
                lastAtkCol = Time.time;
                hp.TakeDamage(1);

            }
        }
    }
}
