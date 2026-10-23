using System.Collections.Generic;
using UnityEngine;

namespace GM033
{
    public class ItemCatalogManager : MonoBehaviour
    {
        [SerializeField] private string[] registeredItemIds;

        private static readonly HashSet<string> RegisteredIds = new HashSet<string>();

        private void Awake()
        {
            RegisteredIds.Clear();

            if (registeredItemIds == null)
                return;

            foreach (string itemId in registeredItemIds)
            {
                if (!string.IsNullOrWhiteSpace(itemId))
                    RegisteredIds.Add(itemId);
            }
        }

        public static bool IsRegistered(string itemId)
        {
            return !string.IsNullOrWhiteSpace(itemId) && RegisteredIds.Contains(itemId);
        }
    }
}
