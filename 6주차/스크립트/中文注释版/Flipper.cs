using UnityEngine;
using UnityEngine.InputSystem;

// 配方 E：弹珠挡板
// 挂载位置：作为转轴的空物体（长条 Cube 为子物体）
[RequireComponent(typeof(Rigidbody))]
public class Flipper : MonoBehaviour
{
    // 控制此挡板的按键
    public Key key = Key.LeftArrow;
    // 静止角度 · 击打角度（Y 轴）
    public float restAngle = 25f;
    public float hitAngle = -25f;
    // 每秒旋转角度
    public float speed = 900f;

    Rigidbody rb;
    float angle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        angle = restAngle;
    }

    void FixedUpdate()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        bool down = kb[key].isPressed;
        float target = down ? hitAngle : restAngle;
        float step = speed * Time.fixedDeltaTime;
        angle = Mathf.MoveTowards(angle, target, step);
        rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
    }
}