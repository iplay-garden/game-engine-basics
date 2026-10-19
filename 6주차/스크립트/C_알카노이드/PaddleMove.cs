using UnityEngine;
using UnityEngine.InputSystem;

// 레시피 B: 키보드로 좌우 이동
// 붙일 곳: 패들 · 바구니
[RequireComponent(typeof(Rigidbody))]
public class PaddleMove : MonoBehaviour
{
    // 초당 이동 거리
    public float speed = 8f;
    // 가운데(X=0)에서 좌우 끝까지 거리
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