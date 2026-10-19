using UnityEngine;

// 적: 웨이포인트를 따라 이동, 체력이 0이 되면 사라진다
// 붙일 곳: 적 프리팹 (Capsule)
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int hp = 3;
    public int reward = 10;
    public int score = 10;
    public AudioClip dieClip;
    public GameObject dieEffect;

    // 살아 있는 적 수 (자동으로 센다)
    public static int Alive;

    Transform[] path;
    int index;
    bool dead;

    void OnEnable()  { Alive++; }
    void OnDisable() { Alive--; }

    // WaveSpawner가 만들자마자 부른다
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
            // 마지막 지점(기지)에 도착
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

    // Tower가 부른다
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
