using UnityEngine;

// 砖块：被球击中就消失，全部消失即成功
// 挂载位置：每个砖块 Cube
public class Brick : MonoBehaviour
{
    public int score = 10;
    public AudioClip breakClip;
    public GameObject breakEffect;

    // 剩余砖块数（自动计数）
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
