using UnityEngine;
using UnityEngine.AI;

namespace GM025
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class GM025Enemy : MonoBehaviour
    {
        float health;
        float damage;
        float nextAttack;
        GM025Player target;
        NavMeshAgent agent;

        public void Initialize(RoomData data, GM025Player player)
        {
            target = player;
            health = data.enemyHealth;
            damage = data.attackDamage;
            transform.localScale = Vector3.one * data.enemyScale;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = data.moveSpeed;
            agent.angularSpeed = 720f;
            agent.acceleration = 16f;
            agent.radius = 0.45f * data.enemyScale;
            agent.height = 1.8f * data.enemyScale;
            GetComponent<Renderer>().material.color = data.enemyColor;
        }

        void Update()
        {
            if (target == null || !agent.isOnNavMesh) return;
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) >= 1.25f + agent.radius || Time.time < nextAttack) return;
            nextAttack = Time.time + 0.8f;
            target.TakeDamage(damage);
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health > 0f) return;
            GM025Game.Instance.EnemyDefeated();
            Destroy(gameObject);
        }
    }
}
