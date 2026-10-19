using UnityEngine;
using UnityEngine.InputSystem;

// 레시피 E: 플리퍼
// 붙일 곳: 회전축 빈 오브젝트 (막대 Cube는 자식)
[RequireComponent(typeof(Rigidbody))]
public class Flipper : MonoBehaviour
{
    // 이 플리퍼를 움직일 키
    public Key key = Key.LeftArrow;
    // 쉬는 각도 · 쳐올린 각도 (Y축)
    public float restAngle = 25f;
    public float hitAngle = -25f;
    // 초당 회전 각도
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