using UnityEngine;

// 动画连接 ③-2：被攻击的物体（敌人 · 箱子等）
// 挂载位置：要被打的物体（需要 Collider）— 挂在 Chaser 敌人上就是“打掉敌人”
public class Hittable : MonoBehaviour
{
    // 被打几次后消失
    public int hp = 1;
    // 消失时得的分数
    public int score = 20;
    public AudioClip clip;
    public GameObject effect;

    bool dead;

    // 由 PlayerAttack 调用
    public void TakeHit()
    {
        if (dead || !GameManager.IsPlaying) return;

        hp--;
        if (hp > 0) return;

        dead = true;
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);
        Destroy(gameObject);
    }
}
