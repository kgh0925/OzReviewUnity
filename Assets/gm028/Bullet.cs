using UnityEngine;

namespace GM028
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 9f;
        [SerializeField] private float lifeTime = 3f;

        private Vector2 direction;

        public void Initialize(Vector2 shootDirection)
        {
            direction = shootDirection.normalized;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.isTrigger)
                Destroy(gameObject);
        }
    }
}
