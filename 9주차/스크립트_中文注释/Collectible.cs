using UnityEngine;

// 规则部件 ① 收集：玩家碰到就加分并消失，全部收集即成功
// 挂载位置：要收集的物体（勾选 Collider 的 Is Trigger）
public class Collectible : MonoBehaviour
{
    // 收集一个的分数
    public int score = 10;
    // 全部收集后是否以成功结束
    public bool winWhenAllCollected = true;
    // 原地旋转速度（0 为不转）
    public float spinSpeed = 90f;
    public AudioClip clip;
    public GameObject effect;

    // 剩余数量（自动计数）
    static int remaining;

    void OnEnable()  { remaining++; }
    void OnDisable() { remaining--; }

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);

        gameObject.SetActive(false);   // 这里 remaining 减 1
        Destroy(gameObject);

        gm.SetInfo("LEFT " + remaining);
        if (winWhenAllCollected && remaining <= 0) gm.EndGame(true);
    }
}
