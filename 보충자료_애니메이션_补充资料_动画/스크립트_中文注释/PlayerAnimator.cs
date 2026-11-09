using UnityEngine;

// 动画连接 ①：把玩家的移动 · 胜负告诉 Animator
// 挂载位置：Player（有 Rigidbody 的物体）
// Animator 参数：Speed (Float) · Win (Trigger) · Lose (Trigger)
public class PlayerAnimator : MonoBehaviour
{
    // 播放动画的 Animator（Robot · 模型那边）。留空就自动从子物体里找
    public Animator animator;

    Rigidbody rb;
    bool wasPlaying;
    bool ended;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        bool playing = GameManager.IsPlaying;

        // 在地面上移动的快慢（不算上下方向的速度）
        Vector3 v = rb != null ? rb.linearVelocity : Vector3.zero;
        v.y = 0f;
        animator.SetFloat("Speed", playing ? v.magnitude : 0f);

        // 从“游戏中”变成“结束”的那一刻只执行一次：成功就 Win，失败就 Lose
        if (wasPlaying && !playing && !ended)
        {
            ended = true;
            GameManager gm = GameManager.Instance;
            // 结果画面的标题和成功文字（CLEAR!）一样就是成功
            bool win = gm != null && gm.resultTitle != null && gm.resultTitle.text == gm.winMessage;
            animator.SetTrigger(win ? "Win" : "Lose");
        }
        wasPlaying = playing;
    }
}
