using UnityEngine;

namespace GM027
{
    public class MainCore : MonoBehaviour
    {
        public const int TargetPower = 100;
        public int TotalPower { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsUnlocked { get; private set; }

        public void ReceivePower(int amount)
        {
            if (IsFinished) return;

            TotalPower += amount;
            Debug.Log("전력 +" + amount + " / 현재 메인 코어 전력: " + TotalPower + " / " + TargetPower);

            if (TotalPower == TargetPower)
            {
                IsFinished = true;
                IsUnlocked = true;
                Debug.Log("메인 코어 안정화 성공!");
            }
            else if (TotalPower > TargetPower)
            {
                IsFinished = true;
                Debug.Log("메인 코어 과부하 실패!");
            }
        }

    }
}
