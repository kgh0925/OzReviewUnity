using UnityEngine;

namespace GM028
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float attackCooldown = 0.5f;

        private PlayerMovement movement;
        private float nextAttackTime;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime)
                Shoot();
        }

        private void Shoot()
        {
            Vector2 direction = movement.FacingDirection;
            Vector3 spawnPosition = transform.position + (Vector3)(direction * 0.7f);
            GameObject bulletObject = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().Initialize(direction);
            nextAttackTime = Time.time + attackCooldown;
            Debug.Log("탄환 생성 위치: " + spawnPosition + " / 방향: " + direction);
        }
    }
}
