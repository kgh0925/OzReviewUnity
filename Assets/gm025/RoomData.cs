using UnityEngine;

namespace GM025
{
    [CreateAssetMenu(menuName = "GM025/Room Data", fileName = "RoomData")]
    public class RoomData : ScriptableObject
    {
        [Min(1)] public int enemyCount = 1;
        [Min(1f)] public float enemyHealth = 30f;
        [Min(0.1f)] public float moveSpeed = 2f;
        [Min(0f)] public float attackDamage = 8f;
        [Min(0.2f)] public float enemyScale = 1f;
        public Color enemyColor = new Color(0.8f, 0.2f, 0.15f);
    }
}
