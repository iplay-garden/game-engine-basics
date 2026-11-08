using UnityEngine;

// 通用：球（Player 标签）进入时结束游戏的区域
// 终点勾选 Is Win，坠落区不勾选
// 挂载位置：Collider 勾选了 Is Trigger 的物体
public class EndZone : MonoBehaviour
{
    public bool isWin = false;
    // 成功时每剩余 1 秒的奖励分
    public int bonusPerSecond = 10;
    public AudioClip clip;
    public GameObject effect;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (isWin)
        {
            gm.AddScore(Mathf.CeilToInt(gm.TimeLeft) * bonusPerSecond);
        }
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, other.transform.position);
        gm.EndGame(isWin);
    }
}
