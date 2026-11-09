using UnityEngine;

// 规则部件 ② 躲避（掉落）：从上方不断掉下物体，并逐渐加快
// 挂载位置：空物体 Spawner（位置 = 掉落区域的中心）
public class FallingSpawner : MonoBehaviour
{
    // 要掉落的预制体（挂有 Rigidbody + Hazard）
    public GameObject prefab;
    // 从中心向左右 · 前后扩展的距离
    public float rangeX = 4f;
    public float rangeZ = 4f;
    // 初始间隔（秒）→ 最短间隔（秒）
    public float startInterval = 1.2f;
    public float minInterval = 0.3f;
    // 每秒间隔缩短的量
    public float speedUp = 0.02f;
    // 掉落物被删除的时间（秒）
    public float lifeTime = 5f;

    float interval;
    float timer;

    void Start()
    {
        interval = startInterval;
    }

    void Update()
    {
        if (!GameManager.IsPlaying || prefab == null) return;

        timer += Time.deltaTime;
        interval = Mathf.Max(minInterval, interval - speedUp * Time.deltaTime);

        if (timer >= interval)
        {
            timer = 0f;
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-rangeX, rangeX), 0f, Random.Range(-rangeZ, rangeZ));
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
            Destroy(obj, lifeTime);
        }
    }

    // 在 Scene 视图中用黄色方框显示掉落区域
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(rangeX * 2f, 0.1f, rangeZ * 2f));
    }
}
