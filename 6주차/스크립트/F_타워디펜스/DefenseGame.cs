using UnityEngine;

// 타워 디펜스 규칙: 골드 · 기지 체력 · 웨이브 표시
// 붙일 곳: 빈 오브젝트 DefenseGame
public class DefenseGame : MonoBehaviour
{
    public static DefenseGame Instance;

    public int gold = 100;
    public int baseHp = 10;
    public AudioClip baseHitClip;

    int wave;
    int totalWaves;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Refresh();
    }

    public bool Spend(int cost)
    {
        if (gold < cost) return false;
        gold -= cost;
        Refresh();
        return true;
    }

    public void Earn(int amount)
    {
        gold += amount;
        Refresh();
    }

    public void SetWave(int current, int total)
    {
        wave = current;
        totalWaves = total;
        Refresh();
    }

    // 적이 기지에 닿으면 Enemy가 부른다
    public void DamageBase()
    {
        if (!GameManager.IsPlaying) return;
        baseHp--;
        GameManager.Instance.PlaySfx(baseHitClip);
        Refresh();
        if (baseHp <= 0) GameManager.Instance.EndGame(false);
    }

    void Refresh()
    {
        GameManager.Instance.SetInfo("WAVE " + wave + "/" + totalWaves
            + "   GOLD " + gold + "   BASE " + baseHp);
    }
}
