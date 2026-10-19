using UnityEngine;

// 벽돌: 공에 맞으면 사라지고, 다 사라지면 성공
// 붙일 곳: 각 벽돌 Cube
public class Brick : MonoBehaviour
{
    public int score = 10;
    public AudioClip breakClip;
    public GameObject breakEffect;

    // 남은 벽돌 수 (자동으로 센다)
    static int remaining;

    void OnEnable()  { remaining++; }
    void OnDisable() { remaining--; }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(breakClip);
        gm.SpawnFx(breakEffect, transform.position);

        gameObject.SetActive(false);
        Destroy(gameObject);

        if (remaining <= 0) gm.EndGame(true);
    }
}
