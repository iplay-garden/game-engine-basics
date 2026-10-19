using UnityEngine;

// 敌人：沿路径点移动，生命为 0 时消失
// 挂载位置：敌人预制体（Capsule）
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int hp = 3;
    public int reward = 10;
    public int score = 10;
    public AudioClip dieClip;
    public GameObject dieEffect;

    // 存活敌人数（自动计数）
    public static int Alive;

    Transform[] path;
    int index;
    bool dead;

    void OnEnable()  { Alive++; }
    void OnDisable() { Alive--; }

    // WaveSpawner 生成后立即调用
    public void Init(Transform[] waypoints)
    {
        path = waypoints;
        index = 1;
        transform.position = path[0].position;
    }

    void Update()
    {
        if (path == null || dead || !GameManager.IsPlaying) return;

        if (index >= path.Length)
        {
            // 到达最后一个点（基地）
            dead = true;
            DefenseGame.Instance.DamageBase();
            Destroy(gameObject);
            return;
        }

        Vector3 target = path[index].position;
        transform.position = Vector3.MoveTowards(
            transform.position, target, speed * Time.deltaTime);

        if ((transform.position - target).sqrMagnitude < 0.0001f) index++;
    }

    // 由 Tower 调用
    public void TakeDamage(int damage)
    {
        if (dead) return;
        hp -= damage;
        if (hp > 0) return;

        dead = true;
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(dieClip);
        gm.SpawnFx(dieEffect, transform.position);
        DefenseGame.Instance.Earn(reward);
        Destroy(gameObject);
    }
}
