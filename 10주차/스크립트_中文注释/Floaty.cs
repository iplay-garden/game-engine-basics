using UnityEngine;

// （挑战）美化部件：在原地上下轻轻浮动
// 挂载位置：只负责外观的子物体 Model（不是父物体，是子物体！）
// 父物体（Collider · 规则脚本）不动，所以碰到的位置不变
public class Floaty : MonoBehaviour
{
    // 上下移动的高度（米）
    public float height = 0.2f;
    // 每秒上下的次数
    public float speed = 1f;

    Vector3 startLocal;   // 初始位置（相对父物体）
    float offset;         // 让每个物体的节奏不同

    void Start()
    {
        startLocal = transform.localPosition;
        offset = Random.Range(0f, 10f);
    }

    void Update()
    {
        // 即使父物体在旋转或是躺着的，也始终“上下”移动
        Vector3 basePos = startLocal;
        Transform parent = transform.parent;
        if (parent != null) basePos = parent.TransformPoint(startLocal);

        // Sin 在 -1 ~ 1 之间平滑往返
        float t = (Time.time + offset) * speed * 2f * Mathf.PI;
        float y = Mathf.Sin(t) * height;
        transform.position = basePos + Vector3.up * y;
    }
}
