using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace GM025
{
    public class GM025Game : MonoBehaviour
    {
        public static GM025Game Instance { get; private set; }
        public RoomData[] roomData;
        public Transform[] spawnRoots;
        public GM025Door[] exitDoors;
        public GM025Player player;

        int currentRoom = -1;
        int enemiesLeft;

        void Awake() => Instance = this;

        void Start() => StartRoom(0);

        public void EnterRoom(int roomIndex)
        {
            if (roomIndex == currentRoom + 1 && roomIndex < roomData.Length)
                StartRoom(roomIndex);
        }

        void StartRoom(int roomIndex)
        {
            currentRoom = roomIndex;
            RoomData data = roomData[roomIndex];
            enemiesLeft = data.enemyCount;
            Debug.Log($"[GM025] Room {roomIndex + 1} start - {enemiesLeft} enemies");

            for (int i = 0; i < data.enemyCount; i++)
            {
                Transform point = spawnRoots[roomIndex].GetChild(i % spawnRoots[roomIndex].childCount);
                GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                enemy.name = $"Room{roomIndex + 1}_Enemy_{i + 1}";
                enemy.transform.position = point.position;
                enemy.AddComponent<NavMeshAgent>();
                enemy.AddComponent<GM025Enemy>().Initialize(data, player);
            }
        }

        public void EnemyDefeated()
        {
            enemiesLeft--;

            if (enemiesLeft != 0) return;

            if (currentRoom < exitDoors.Length)
            {
                exitDoors[currentRoom].Open();
                return;
            }

            Debug.Log("Å¬¸®¾î");
        }

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
