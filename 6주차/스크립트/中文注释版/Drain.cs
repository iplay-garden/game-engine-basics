using UnityEngine;

// 弹珠台底部出口：球掉入则剩余球数 -1，为 0 时结束
// 挂载位置：挡板下方勾选 Is Trigger 的区域
public class Drain : MonoBehaviour
{
    public Transform spawnPoint;
    public int balls = 3;
    // 达到此分数为 CLEAR，否则 GAME OVER
    public int targetScore = 1000;
    public AudioClip loseBallClip;

    void Start()
    {
        ShowInfo();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        balls--;
        gm.PlaySfx(loseBallClip);
        ShowInfo();

        if (balls <= 0)
        {
            gm.EndGame(gm.Score >= targetScore);
            return;
        }

        // 把球送回出发位置
        Rigidbody rb = other.attachedRigidbody;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = spawnPoint.position;
    }

    void ShowInfo()
    {
        string info = "BALL " + balls + "   GOAL " + targetScore;
        GameManager.Instance.SetInfo(info);
    }
}
