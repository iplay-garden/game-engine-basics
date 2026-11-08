using UnityEngine;

// 规则部件 ⑤-2 门：拿着钥匙碰到即成功
// 挂载位置：门 · 出口（勾选 Collider 的 Is Trigger）
public class GoalDoor : MonoBehaviour
{
    // 剩余时间每秒的奖励分数
    public int bonusPerSecond = 10;
    public AudioClip clip;
    public GameObject effect;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (!KeyItem.HasKey)
        {
            gm.SetInfo("FIND THE KEY FIRST");
            return;
        }
        gm.AddScore(Mathf.CeilToInt(gm.TimeLeft) * bonusPerSecond);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);
        gm.EndGame(true);
    }
}
