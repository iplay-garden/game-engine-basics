using UnityEngine;
using UnityEngine.InputSystem;

// 配方 B：键盘左右移动
// 挂载位置：挡板 · 篮子
[RequireComponent(typeof(Rigidbody))]
public class PaddleMove : MonoBehaviour
{
    // 每秒移动距离
    public float speed = 8f;
    // 从中心(X=0)到左右边界的距离
    public float limitX = 4f;

    Rigidbody rb;
    float dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        dir = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) dir -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir += 1f;
    }

    void FixedUpdate()
    {
        Vector3 pos = rb.position;
        pos.x += dir * speed * Time.fixedDeltaTime;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        rb.MovePosition(pos);
    }
}