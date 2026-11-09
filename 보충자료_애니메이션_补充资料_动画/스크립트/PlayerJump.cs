using UnityEngine;
using UnityEngine.InputSystem;

// 애니메이션 연결 ②: Space로 점프 + Jump 애니메이션
// 붙일 곳: Player (Rigidbody 필요)
// Animator 파라미터: Jump (Trigger)
[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    // 위로 튀어 오르는 힘 (속도)
    public float jumpPower = 5f;
    // 발밑 확인 거리 (Capsule 가운데에서 바닥까지 1 + 여유)
    public float groundCheck = 1.1f;
    // 비우면 자식에서 자동으로 찾는다
    public Animator animator;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!GameManager.IsPlaying) return;

        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            Vector3 v = rb.linearVelocity;
            v.y = jumpPower;
            rb.linearVelocity = v;
            if (animator != null) animator.SetTrigger("Jump");
        }
    }

    // 발밑으로 짧은 선을 쏴서 바닥에 닿아 있는지 확인 (Is Trigger 구역은 무시)
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheck, ~0, QueryTriggerInteraction.Ignore);
    }
}
