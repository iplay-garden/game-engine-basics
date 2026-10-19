using UnityEngine;
using UnityEngine.InputSystem;

// 配方 C：倾斜底板
// 挂载位置：空物体 Board（底板 · 墙作为子物体）
[RequireComponent(typeof(Rigidbody))]
public class TiltBoard : MonoBehaviour
{
    // 最大倾斜角度
    public float maxAngle = 12f;
    // 每秒倾斜角度
    public float tiltSpeed = 40f;

    Rigidbody rb;
    float angleX, angleZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f, v = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) v += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) v -= 1f;

        angleX += v * tiltSpeed * Time.deltaTime;
        angleZ -= h * tiltSpeed * Time.deltaTime;
        angleX = Mathf.Clamp(angleX, -maxAngle, maxAngle);
        angleZ = Mathf.Clamp(angleZ, -maxAngle, maxAngle);
    }

    void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(angleX, 0f, angleZ));
    }
}