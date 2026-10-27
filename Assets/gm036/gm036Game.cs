using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    INIT,
    STAGE_INTRO,
    WAVE_READY,
    WAVE_PROGRESS,
    WAVE_INTERVAL,
    STAGE_CLEAR,
    STAGE_FAILED,
    PAUSE
}

public class gm036Game : MonoBehaviour
{
    [Header("State")]
    public GameState currentState;
    public int totalWaves = 3;

    [Header("UI")]
    public GameObject stageIntroPopup;
    public GameObject waveReadyPopup;
    public GameObject stageClearPopup;
    public GameObject stageFailedPopup;
    public GameObject pausePopup;
    public GameObject failButton;
    public Text stateText;
    public Text waveText;
    public Text countdownText;

    private int currentWave;
    private GameState stateBeforePause;

    private void Start()
    {
        ChangeState(GameState.INIT);
    }

    public void ChangeState(GameState nextState)
    {
        StopAllCoroutines();
        currentState = nextState;
        Time.timeScale = 1f;

        stageIntroPopup.SetActive(nextState == GameState.STAGE_INTRO);
        waveReadyPopup.SetActive(nextState == GameState.WAVE_READY);
        stageClearPopup.SetActive(nextState == GameState.STAGE_CLEAR);
        stageFailedPopup.SetActive(nextState == GameState.STAGE_FAILED);
        pausePopup.SetActive(nextState == GameState.PAUSE);
        failButton.SetActive(nextState == GameState.WAVE_PROGRESS);

        stateText.text = "State : " + nextState;
        waveText.text = currentWave > 0 ? $"Wave : {currentWave} / {totalWaves}" : "Wave : -";

        switch (nextState)
        {
            case GameState.INIT:
                currentWave = 0;
                StartCoroutine(ChangeAfter(2f, GameState.STAGE_INTRO));
                break;
            case GameState.WAVE_READY:
                currentWave++;
                waveText.text = $"Wave : {currentWave} / {totalWaves}";
                StartCoroutine(WaveCountdown());
                break;
            case GameState.WAVE_PROGRESS:
                StartCoroutine(ChangeAfter(5f, GameState.WAVE_INTERVAL));
                break;
            case GameState.WAVE_INTERVAL:
                StartCoroutine(NextWaveAfterInterval());
                break;
        }
    }

    public void OnStartButton() => ChangeState(GameState.WAVE_READY);

    public void OnFailButton()
    {
        if (currentState == GameState.WAVE_PROGRESS)
            ChangeState(GameState.STAGE_FAILED);
    }

    public void OnPauseButton()
    {
        if (currentState == GameState.PAUSE || currentState == GameState.STAGE_CLEAR || currentState == GameState.STAGE_FAILED)
            return;

        stateBeforePause = currentState;
        currentState = GameState.PAUSE;
        pausePopup.SetActive(true);
        stateText.text = "State : PAUSE";
        Time.timeScale = 0f;
    }

    public void OnResumeButton()
    {
        if (currentState != GameState.PAUSE)
            return;

        currentState = stateBeforePause;
        pausePopup.SetActive(false);
        stateText.text = "State : " + currentState;
        Time.timeScale = 1f;
    }

    public void OnRestartButton() => ChangeState(GameState.INIT);

    private IEnumerator ChangeAfter(float seconds, GameState nextState)
    {
        yield return new WaitForSeconds(seconds);
        ChangeState(nextState);
    }

    private IEnumerator WaveCountdown()
    {
        for (int count = 3; count > 0; count--)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }

        ChangeState(GameState.WAVE_PROGRESS);
    }

    private IEnumerator NextWaveAfterInterval()
    {
        yield return new WaitForSeconds(1f);
        ChangeState(currentWave >= totalWaves ? GameState.STAGE_CLEAR : GameState.WAVE_READY);
    }
}
