using UnityEngine;

public class Switch : MonoBehaviour
{
    public Renderer[] switchs;
    public int gameRound = 10;

    private int gameCount;
    private int answerIndex;
    private int correctCount;

    private bool roundActive;
    private bool messageShown;

    void Update()
    {
        if (gameCount >= gameRound)
        {
            float accuracy = (float)correctCount / gameRound * 100f;

            Debug.Log(
                $"정답률 : {correctCount} / {gameRound} [{accuracy:F1}%]"
            );

            gameObject.SetActive(false);
            return;
        }

        if (!roundActive)
        {
            PrepareRound();
            return;
        }

        // 치트키는 라운드를 종료하지 않음
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(
                $"현재 라운드의 정답은 {answerIndex + 1}번 스위치입니다."
            );
        }

        int selectedIndex = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedIndex = 2;

        // 아직 스위치를 선택하지 않았다면 라운드 유지
        if (selectedIndex == -1)
            return;

        if (selectedIndex == answerIndex)
        {
            correctCount++;
            switchs[selectedIndex].material.color = Color.green;
        }
        else
        {
            switchs[selectedIndex].material.color = Color.red;

            // 오답일 때 정답까지 표시하려는 경우
            switchs[answerIndex].material.color = Color.green;
        }

        gameCount++;
        roundActive = false;
    }

    void PrepareRound()
    {
        if (!messageShown)
        {
            Debug.Log("아무 키를 눌러 다음 라운드를 준비하세요.");
            messageShown = true;
        }

        if (!Input.anyKeyDown)
            return;

        for (int i = 0; i < switchs.Length; i++)
        {
            switchs[i].material.color = Color.gray;
        }

        answerIndex = Random.Range(0, switchs.Length);
        roundActive = true;
        messageShown = false;

        Debug.Log($"{gameCount + 1}라운드 시작");
    }
}