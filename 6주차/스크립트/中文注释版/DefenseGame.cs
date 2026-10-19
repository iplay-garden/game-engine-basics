using UnityEngine;

// 塔防规则：金币 · 基地生命 · 波次显示
// 挂载位置：空物体 DefenseGame
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

    // 敌人到达基地时由 Enemy 调用
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
