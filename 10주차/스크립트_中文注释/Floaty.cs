using UnityEngine;

// （挑战）美化部件：在原地上下轻轻浮动，并慢慢旋转
// 挂载位置：只负责外观的子物体 Model（不是父物体，是子物体！）
// 父物体（Collider · 规则脚本）不动，所以碰到的位置不变
public class Floaty : MonoBehaviour
{
    // 上下移动的高度（米）
    public float height = 0.2f;
    // 每秒上下的次数
    public float speed = 1f;
    // 每秒旋转的角度（0 = 不旋转）
    public float spinSpeed = 0f;

    Vector3 startPos;   // 初始位置（相对父物体）
    float offset;       // 让每个物体的节奏不同

    void Start()
    {
        startPos = transform.localPosition;
        offset = Random.Range(0f, 10f);
    }

    void Update()
    {
        // Sin 在 -1 ~ 1 之间平滑往返 → 上下移动
        float t = (Time.time + offset) * speed * 2f * Mathf.PI;
        float y = Mathf.Sin(t) * height;
        transform.localPosition = startPos + new Vector3(0f, y, 0f);
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }
}
