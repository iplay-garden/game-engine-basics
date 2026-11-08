using UnityEngine;

// 规则部件 ② 躲避：玩家碰到就失败
// 挂载位置：障碍物 · 掉落物 · 敌人（需要 Collider）
public class Hazard : MonoBehaviour
{
    public AudioClip clip;
    public GameObject effect;

    // 实体物体相撞时
    void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    // 勾选了 Is Trigger 时
    void OnTriggerEnter(Collider other)
    {
        Hit(other.gameObject);
    }

    void Hit(GameObject target)
    {
        if (!target.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, target.transform.position);
        gm.EndGame(false);
    }
}
