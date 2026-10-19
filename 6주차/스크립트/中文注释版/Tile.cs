using UnityEngine;

// 放置格：点击后花金币建塔
// 挂载位置：可以建塔的地面格子
public class Tile : MonoBehaviour
{
    public GameObject towerPrefab;
    public int cost = 50;
    // 塔相对格子中心的高度
    public Vector3 offset = new Vector3(0f, 0.7f, 0f);
    public AudioClip buildClip;
    public AudioClip failClip;
    public GameObject buildEffect;

    bool occupied;

    // ClickHit 发送的消息
    void OnClicked()
    {
        if (occupied || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (!DefenseGame.Instance.Spend(cost))
        {
            gm.PlaySfx(failClip);
            return;
        }

        occupied = true;
        Vector3 pos = transform.position + offset;
        Instantiate(towerPrefab, pos, Quaternion.identity);
        gm.PlaySfx(buildClip);
        gm.SpawnFx(buildEffect, pos);
    }
}
