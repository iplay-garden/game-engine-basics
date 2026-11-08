using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

// 공통: 타이틀 → 플레이 → 결과 흐름, 점수 · 시간 · 소리 · 이펙트
// 붙일 곳: 빈 오브젝트 GameManager
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsPlaying
    {
        get { return Instance != null && Instance.playing; }
    }

    [Header("Panels")]
    public GameObject titlePanel;
    public GameObject hudPanel;
    public GameObject resultPanel;

    [Header("Texts")]
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text infoText;
    public TMP_Text resultTitle;
    public TMP_Text resultScore;
    public string winMessage = "CLEAR!";
    public string loseMessage = "GAME OVER";

    [Header("Time")]
    // 0이면 시간 제한 없음
    public float timeLimit = 60f;
    // 체크: 시간이 끝나면 성공 (두더지처럼 버티는 게임)
    public bool winWhenTimeUp = false;

    [Header("Sound")]
    public AudioSource sfxSource;
    public AudioClip winClip;
    public AudioClip loseClip;

    public int Score { get; private set; }
    public float TimeLeft { get; private set; }

    bool playing;
    static bool skipTitle;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (skipTitle)
        {
            skipTitle = false;
            StartGame();
        }
        else
        {
            // 타이틀에서는 시간을 멈춰 둔다
            Time.timeScale = 0f;
            ShowPanel(titlePanel);
        }
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb != null && kb.escapeKey.wasPressedThisFrame)
        {
            // 빌드에서 ESC = 게임 종료 (에디터에서는 무시됨)
            Application.Quit();
        }

        if (!playing || timeLimit <= 0f) return;

        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0f)
        {
            TimeLeft = 0f;
            EndGame(winWhenTimeUp);
        }
        RefreshHud();
    }

    // 타이틀의 START 버튼에 연결
    public void StartGame()
    {
        Time.timeScale = 1f;
        Score = 0;
        TimeLeft = timeLimit;
        playing = true;
        ShowPanel(hudPanel);
        RefreshHud();
    }

    public void AddScore(int amount)
    {
        if (!playing) return;
        Score += amount;
        RefreshHud();
    }

    public void SetInfo(string message)
    {
        if (infoText != null) infoText.text = message;
    }

    // 성공이면 true, 실패면 false
    public void EndGame(bool win)
    {
        if (!playing) return;
        playing = false;
        ShowPanel(resultPanel);
        if (resultTitle != null)
            resultTitle.text = win ? winMessage : loseMessage;
        if (resultScore != null)
            resultScore.text = "SCORE " + Score;
        PlaySfx(win ? winClip : loseClip);
    }

    // 결과의 RETRY 버튼에 연결 (타이틀 없이 바로 다시 시작)
    public void Retry()
    {
        skipTitle = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 결과의 TITLE 버튼에 연결 (선택)
    public void BackToTitle()
    {
        skipTitle = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (sfxSource != null && clip != null) sfxSource.PlayOneShot(clip);
    }

    public void SpawnFx(GameObject effectPrefab, Vector3 position)
    {
        if (effectPrefab == null) return;
        Instantiate(effectPrefab, position, Quaternion.identity);
    }

    void RefreshHud()
    {
        if (scoreText != null) scoreText.text = "SCORE " + Score;
        if (timeText != null)
        {
            int sec = Mathf.CeilToInt(TimeLeft);
            timeText.text = timeLimit > 0f ? "TIME " + sec : "";
        }
    }

    void ShowPanel(GameObject target)
    {
        if (titlePanel != null) titlePanel.SetActive(titlePanel == target);
        if (hudPanel != null) hudPanel.SetActive(hudPanel == target);
        if (resultPanel != null) resultPanel.SetActive(resultPanel == target);
    }
}
