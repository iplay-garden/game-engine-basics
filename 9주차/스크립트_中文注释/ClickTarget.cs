using UnityEngine;

// 规则部件 ③ 击中：点击得分，并瞬间移动到别处
// 挂载位置：靶子（需要 Collider）· Main Camera 上挂 ClickHit
public class ClickTarget : MonoBehaviour
{
    public int score = 10;
    // 重新出现的范围（以初始位置为准）
    public float rangeX = 5f;
    public float rangeY = 2f;
    public AudioClip clip;
    public GameObject effect;

    Vector3 home;

    void Start()
    {
        home = transform.position;
    }

    // ClickHit 发送的消息
    void OnClicked()
    {
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);

        transform.position = home + new Vector3(
            Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0f);
    }
}
