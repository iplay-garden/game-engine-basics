using UnityEngine;
using UnityEngine.InputSystem;

// 레시피 C: 판 기울이기
// 붙일 곳: 빈 오브젝트 Board (판 · 벽은 자식으로)
[RequireComponent(typeof(Rigidbody))]
public class TiltBoard : MonoBehaviour
{
    // 최대 기울기(도)
    public float maxAngle = 12f;
    // 초당 기울어지는 각도
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