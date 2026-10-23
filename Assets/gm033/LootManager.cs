/*using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
public struct ItemData
{
    public int id; public string name;
}
public class LootManager : MonoBehaviour
{

    public LayerMask obstacleLayer; // 장애물 레이어 (인스펙터 할당)

    // [빈칸 A] 몬스터 고유 ID(string)를 기반으로 드랍 정보 목록(List<DropEntry>)을 관리하는 자료구조
    private Dictionary<string, List<DropEntry>> lootTableDB;

    // [빈칸 B] 런타임 중 크기나 값이 변하지 않는 8방향의 '기준 방향 벡터' 데이터
    private Vector2[] baseDirections = {
        new Vector2(0, 1).normalized, new Vector2(1, 1).normalized, new Vector2(1, 0).normalized, new Vector2(1, -1).normalized,
        new Vector2(0, -1).normalized, new Vector2(-1, -1).normalized, new Vector2(-1, 0).normalized, new Vector2(-1, 1).normalized
    };

    public void OnMonsterDeath(string monsterID, Vector2 deathPosition)
    {
        if (!lootTableDB.ContainsKey(monsterID)) return;
        List<DropEntry> table = lootTableDB[monsterID];

        // [빈칸 C] 확률 계산을 통과하여 실제로 드랍될 아이템 데이터를 임시로 담아둘 동적 자료구조 할당
        List<ItemData> itemsToDrop = new List<ItemData>();

        foreach (DropEntry entry in table)
        {
            if (Random.Range(0f, 100f) <= entry.dropProbability)
            {
                itemsToDrop.Add(entry.itemData);
            }
        }

        SpawnItemsDynamically(itemsToDrop, deathPosition);
    }

    private void SpawnItemsDynamically(List<ItemData> items, Vector2 centerPos)
    {
        int spawnedCount = 0;
        int currentRadius = 1;  // 시작 궤도 반경
        int attemptCount = 0;   // 탐색 횟수 (무한 루프 방지용)
        int maxAttempts = 100;

        // 드랍해야 할 아이템을 모두 스폰할 때까지 반복 (단, 최대 탐색 횟수 제한)
        while (spawnedCount < items.Count && attemptCount < maxAttempts)
        {
            // [빈칸 D] baseDirections 배열을 순환하며 현재 시도할 방향 벡터를 가져오는 코드 (나머지 연산자 활용)
            Vector2 currentDir = baseDirections[attemptCount % baseDirections.Length];

            // [빈칸 E] 중심 좌표(centerPos)에서 현재 방향(currentDir)으로 현재 반경(currentRadius)만큼 떨어진 최종 목표 좌표 계산
            Vector2 targetPos = centerPos ;

            // [빈칸 F] 계산된 targetPos를 중심으로 반경 0.3f 내에 obstacleLayer에 해당하는 2D 콜라이더가 있는지 검사하는 물리 API 호출
            bool isBlocked = Physics2D.OverlapCircle(targetPos,0.3f,obstacleLayer);

            // 장애물이 없다면 아이템 스폰
            if (!isBlocked)
            {
                Instantiate(items[spawnedCount].prefab, targetPos, Quaternion.identity);
                spawnedCount++; // 스폰 성공 시에만 카운트 증가
            }

            attemptCount++;

            // 8방향(한 바퀴)를 모두 탐색했다면, 다음 궤도로 넘어가기 위해 반경(Radius) 증가
            if (attemptCount % baseDirections.Length == 0)
            {
                currentRadius++;
            }
        }
    }
}

[System.Serializable]
public struct DropEntry
{
    public ItemData itemData;
    public float dropProbability;
}
*/