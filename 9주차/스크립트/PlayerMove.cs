using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 이동 (WASD · 방향키) — 위에서 내려다보는 3D 게임용
// 붙일 곳: 플레이어 (Rigidbody + Collider 필요)
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    // 초당 이동 속도
    public float speed = 5f;
    // 이동 방향을 바라볼 모델 (자식 오브젝트, 비워도 됨)
    public Transform model;

    Rigidbody rb;
    Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 부딪혀도 넘어지지 않게 회전을 막는다
        rb.constraints = RigidbodyConstraints.FreezeRotation;
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
        input = new Vector3(h, 0f, v).normalized;

        // 움직이는 방향으로 모델을 돌린다
        if (model != null && input != Vector3.zero) model.forward = input;
    }

    void FixedUpdate()
    {
        Vector3 vel = input * speed;
        vel.y = rb.linearVelocity.y;   // 떨어지는 속도는 그대로 둔다
        rb.linearVelocity = vel;
    }
}
