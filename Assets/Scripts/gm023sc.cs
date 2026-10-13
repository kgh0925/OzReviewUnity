using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class gm023sc : MonoBehaviour
{
    private enum GameState
    {
        ShowingPattern,
        WaitingForInput,
        RoundClear,
        GameWon,
        GameLost
    }

    [Header("Pattern")]
    [SerializeField] private Renderer patternCube;
    [SerializeField, Min(0f)] private float startDelay = 1f;
    [SerializeField, Min(0f)] private float colorGap = 0.2f;
    [SerializeField, Min(0f)] private float nextRoundDelay = 1f;

    private readonly Color[] colors =
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow
    };

    private readonly float[] roundColorTimes = { 1.5f, 1f, 0.5f };
    private readonly int[] pattern = new int[4];

    private GameState state;
    private Material cubeMaterial;
    private int currentRound;
    private int showIndex;
    private int inputIndex;
    private float timer;
    private bool showingColor;

    private static readonly Color IdleColor = Color.gray;

    private void Awake()
    {
        if (patternCube == null)
        {
            patternCube = GetComponent<Renderer>();
        }

        cubeMaterial = patternCube.material;
    }

    private void Start()
    {
        RestartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
            return;
        }

        if ((state == GameState.GameWon || state == GameState.GameLost) &&
            Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
            return;
        }

        switch (state)
        {
            case GameState.ShowingPattern:
                UpdatePatternDisplay();
                break;
            case GameState.WaitingForInput:
                UpdatePlayerInput();
                break;
            case GameState.RoundClear:
                UpdateRoundTransition();
                break;
        }
    }

    private void RestartGame()
    {
        currentRound = 0;
        StartRound();
        Debug.Log("패턴 게임 시작 - 숫자키 1~4로 입력하세요.");
    }

    private void StartRound()
    {
        GenerateUniquePattern();
        state = GameState.ShowingPattern;
        showIndex = 0;
        inputIndex = 0;
        timer = startDelay;
        showingColor = false;
        SetCubeColor(IdleColor);

        Debug.Log($"{currentRound + 1}라운드: 패턴을 기억하세요.");
    }

    private void GenerateUniquePattern()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = i;
        }

        for (int i = pattern.Length - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            (pattern[i], pattern[swapIndex]) = (pattern[swapIndex], pattern[i]);
        }
    }

    private void UpdatePatternDisplay()
    {
        timer -= Time.deltaTime;
        if (timer > 0f)
        {
            return;
        }

        if (!showingColor)
        {
            SetCubeColor(colors[pattern[showIndex]]);
            timer = roundColorTimes[currentRound];
            showingColor = true;
            return;
        }

        SetCubeColor(IdleColor);
        showIndex++;
        showingColor = false;

        if (showIndex >= pattern.Length)
        {
            state = GameState.WaitingForInput;
            Debug.Log("입력 시작! 1=빨강, 2=초록, 3=파랑, 4=노랑");
        }
        else
        {
            timer = colorGap;
        }
    }

    private void UpdatePlayerInput()
    {
        int selectedColor = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            selectedColor = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            selectedColor = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            selectedColor = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            selectedColor = 3;

        if (selectedColor < 0)
        {
            return;
        }

        SetCubeColor(colors[selectedColor]);

        if (selectedColor != pattern[inputIndex])
        {
            state = GameState.GameLost;
            SetCubeColor(Color.red);
            Debug.Log("틀렸습니다. R: 재시작 / Esc: 게임 종료");
            return;
        }

        inputIndex++;
        if (inputIndex < pattern.Length)
        {
            return;
        }

        SetCubeColor(Color.green);

        if (currentRound >= roundColorTimes.Length - 1)
        {
            state = GameState.GameWon;
            Debug.Log("모든 3라운드 성공! R: 재시작 / Esc: 게임 종료");
        }
        else
        {
            state = GameState.RoundClear;
            timer = nextRoundDelay;
            Debug.Log($"{currentRound + 1}라운드 성공! 다음 라운드를 준비합니다.");
        }
    }

    private void UpdateRoundTransition()
    {
        timer -= Time.deltaTime;
        if (timer > 0f)
        {
            return;
        }

        currentRound++;
        StartRound();
    }

    private void SetCubeColor(Color color)
    {
        cubeMaterial.color = color;
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void OnDestroy()
    {
        if (cubeMaterial != null)
        {
            Destroy(cubeMaterial);
        }
    }
}
