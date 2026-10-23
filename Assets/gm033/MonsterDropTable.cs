using System;
using System.Collections.Generic;
using UnityEngine;

namespace GM033
{
    [RequireComponent(typeof(HealthPointManager))]
    public class MonsterDropTable : MonoBehaviour
    {
        [Serializable]
        private class DropEntry
        {
            public WorldItem itemPrefab = null;
            [Range(0f, 100f)] public float dropChance = 100f;
        }

        [SerializeField] private DropEntry[] dropTable;
        [SerializeField, Min(0.01f)] private float dropSpacing = 0.5f;

        private HealthPointManager healthPointManager;
        private bool hasDropped;

        private void Awake()
        {
            healthPointManager = GetComponent<HealthPointManager>();
        }

        private void OnEnable()
        {
            hasDropped = false;

            if (healthPointManager == null)
                healthPointManager = GetComponent<HealthPointManager>();

            healthPointManager.OnDied += HandleDied;
        }

        private void OnDisable()
        {
            if (healthPointManager != null)
                healthPointManager.OnDied -= HandleDied;
        }

        private void HandleDied()
        {
            if (hasDropped)
                return;

            hasDropped = true;

            if (dropTable == null || dropTable.Length == 0)
            {
                Debug.LogWarning($"{name}: DropTable이 비어 있습니다.", this);
                return;
            }

            List<WorldItem> droppedPrefabs = new List<WorldItem>();

            foreach (DropEntry entry in dropTable)
            {
                if (entry == null || entry.itemPrefab == null)
                {
                    Debug.LogWarning($"{name}: DropTable에 프리팹이 할당되지 않은 항목이 있습니다.", this);
                    continue;
                }

                if (UnityEngine.Random.Range(0f, 100f) < entry.dropChance)
                    droppedPrefabs.Add(entry.itemPrefab);
            }

            SpawnItems(droppedPrefabs);
        }

        private void SpawnItems(List<WorldItem> itemPrefabs)
        {
            float startX = -(itemPrefabs.Count - 1) * dropSpacing * 0.5f;

            for (int i = 0; i < itemPrefabs.Count; i++)
            {
                Vector3 offset = new Vector3(startX + i * dropSpacing, 0f, 0f);
                Instantiate(itemPrefabs[i], transform.position + offset, Quaternion.identity);
            }
        }
    }
}
