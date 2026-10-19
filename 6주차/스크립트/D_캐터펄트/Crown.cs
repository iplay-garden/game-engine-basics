using UnityEngine;

// 왕관: 정해 둔 높이보다 낮아지면(떨어지면) 성공
// 붙일 곳: 탑 꼭대기의 왕관 (Rigidbody 필요)
public class Crown : MonoBehaviour
{
    // 바닥 높이보다 조금 위로 맞추기
    public float winHeight = 1f;
    public LaunchForce launcher;
    // 남은 기회 1번당 보너스
    public int bonusPerShot = 100;
    public AudioClip winClip;
    public GameObject winEffect;

    void Update()
    {
        if (!GameManager.IsPlaying) return;

        if (transform.position.y < winHeight)
        {
            GameManager gm = GameManager.Instance;
            gm.AddScore(100 + launcher.ShotsLeft * bonusPerShot);
            gm.PlaySfx(winClip);
            gm.SpawnFx(winEffect, transform.position);
            gm.EndGame(true);
        }
    }
}
