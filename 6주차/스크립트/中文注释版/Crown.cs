using UnityEngine;

// 王冠：低于设定高度（掉下来）即成功
// 挂载位置：塔顶的王冠（需要 Rigidbody）
public class Crown : MonoBehaviour
{
    // 设为比地面略高
    public float winHeight = 1f;
    public LaunchForce launcher;
    // 每剩余 1 次机会的奖励
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
