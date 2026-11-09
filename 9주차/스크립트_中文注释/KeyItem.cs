using UnityEngine;

// 规则部件 ④-1 钥匙：玩家碰到就获得钥匙
// 挂载位置：钥匙（勾选 Collider 的 Is Trigger）
public class KeyItem : MonoBehaviour
{
    // 是否拿到钥匙（由 GoalDoor 检查）
    public static bool HasKey;
    public int score = 50;
    public AudioClip clip;

    void Start()
    {
        HasKey = false;   // 重新开始时重置
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        HasKey = true;
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SetInfo("GO TO THE DOOR!");
        gameObject.SetActive(false);
    }
}
