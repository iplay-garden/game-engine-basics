using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

// 通用：标题 → 游戏 → 结果流程，分数 · 时间 · 音效 · 特效
// 挂载位置：空物体 GameManager
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
    // 0 表示不限时
    public float timeLimit = 60f;
    // 勾选：时间到即成功（像打地鼠这种坚持型游戏）
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
            // 标题画面时暂停时间
            Time.timeScale = 0f;
            ShowPanel(titlePanel);
        }
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb != null && kb.escapeKey.wasPressedThisFrame)
        {
            // 打包后按 ESC 退出（编辑器中无效）
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

    // 连接到标题画面的 START 按钮
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

    // 成功传 true，失败传 false
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

    // 连接到结果画面的 RETRY 按钮（跳过标题直接重开）
    public void Retry()
    {
        skipTitle = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 连接到结果画面的 TITLE 按钮（可选）
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
