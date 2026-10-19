using UnityEngine;

// 핀볼 아래 배수구: 공이 빠지면 남은 공 -1, 0이 되면 끝
// 붙일 곳: 플리퍼 아래의 Is Trigger 구역
public class Drain : MonoBehaviour
{
    public Transform spawnPoint;
    public int balls = 3;
    // 이 점수 이상이면 CLEAR, 아니면 GAME OVER
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

        // 공을 출발 위치로 되돌린다
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
