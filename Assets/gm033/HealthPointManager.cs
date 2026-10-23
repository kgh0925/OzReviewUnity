using System;
using UnityEngine;

namespace GM033
{
    public class HealthPointManager : MonoBehaviour
    {
        [SerializeField, Min(1)] private int maxHealth = 10;

        public event Action OnDied;

        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead || damage <= 0)
                return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
            Debug.Log($"몬스터 남은 체력: {CurrentHealth}");

            if (CurrentHealth == 0)
                Die();
        }

        private void Die()
        {
            if (IsDead)
                return;

            IsDead = true;
            OnDied?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

