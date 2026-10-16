using System.Collections.Generic;
using UnityEngine;

public class DungeonExplorer : MonoBehaviour
{
    private readonly string[] monsterPool =
    {
        "늑대",
        "고블린",
        "슬라임",
        "스켈레톤"
    };

    private readonly string[] roomPool =
    {
        "어두운 복도",
        "낡은 제단",
        "축축한 동굴",
        "버려진 무기고"
    };

    private readonly Queue<string> monsterQueue = new Queue<string>();
    private readonly Stack<string> travelHistory = new Stack<string>();

    private void Start()
    {
        Debug.Log("던전 탐험을 시작합니다. [S] 정찰 / [A] 전투 / [M] 이동 / [B] 퇴각");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScoutMonster();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            FightMonster();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveToNextRoom();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Retreat();
        }
    }

    private void ScoutMonster()
    {
        string monster = monsterPool[Random.Range(0, monsterPool.Length)];
        monsterQueue.Enqueue(monster);

        Debug.Log($"[조우 예고] 저 멀리서 {monster}이(가) 나타났습니다!");
    }

    private void FightMonster()
    {
        if (monsterQueue.Count == 0)
        {
            Debug.Log("[전투] 싸울 몬스터가 없습니다.");
            return;
        }

        string monster = monsterQueue.Dequeue();
        Debug.Log($"[전투] {monster}을(를) 처치했습니다! 남은 적: {monsterQueue.Count}마리");
    }

    private void MoveToNextRoom()
    {
        string room = roomPool[Random.Range(0, roomPool.Length)];
        travelHistory.Push(room);

        Debug.Log($"[이동] {room}에 진입했습니다. (총 이동 거리: {travelHistory.Count})");
    }

    private void Retreat()
    {
        if (travelHistory.Count == 0)
        {
            Debug.Log("던전 입구까지 돌아왔습니다. 더 이상 퇴각할 수 없습니다.");
            return;
        }

        travelHistory.Pop();

        if (travelHistory.Count == 0)
        {
            Debug.Log("던전 입구까지 돌아왔습니다. 더 이상 퇴각할 수 없습니다.");
            return;
        }

        string previousRoom = travelHistory.Peek();
        Debug.Log($"[퇴각] {previousRoom}으로 돌아왔습니다.");
    }
}
