using UnityEngine;

namespace GM025
{
    [RequireComponent(typeof(CharacterController))]
    public class GM025Player : MonoBehaviour
    {
        public float moveSpeed = 7f;
        public float maxHealth = 100f;
        public float attackDamage = 20f;
        public float attackRange = 6f;
        public float attackCooldown = 0.5f;

        public float Health { get; private set; }
        CharacterController controller;
        float nextAttack;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            Health = maxHealth;
        }

        void Update()
        {
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            controller.SimpleMove(input * moveSpeed);

            GM025Enemy target = FindNearestEnemy();
            if (target == null) return;
            if (Time.time >= nextAttack) Attack(target);
        }

        GM025Enemy FindNearestEnemy()
        {
            GM025Enemy nearest = null;
            float nearestDistance = attackRange * attackRange;

            foreach (Collider hit in Physics.OverlapSphere(transform.position, attackRange))
            {
                GM025Enemy enemy = hit.GetComponentInParent<GM025Enemy>();
                if (enemy == null) continue;

                float distance = (enemy.transform.position - transform.position).sqrMagnitude;
                if (distance >= nearestDistance) continue;
                nearest = enemy;
                nearestDistance = distance;
            }

            return nearest;
        }

        void Attack(GM025Enemy target)
        {
            nextAttack = Time.time + attackCooldown;
            target.TakeDamage(attackDamage);
        }

        public void TakeDamage(float amount)
        {
            Health = Mathf.Max(0f, Health - amount);
            if (Health > 0f) return;
            GM025Game.Instance.RestartGame();
        }
    }
}
