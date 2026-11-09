using UnityEngine;
using UnityEngine.InputSystem;

// 动画连接 ②：按 Space 跳跃 + Jump 动画
// 挂载位置：Player（需要 Rigidbody）
// Animator 参数：Jump (Trigger)
[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    // 向上跳的力度（速度）
    public float jumpPower = 5f;
    // 检查脚下的距离（从 Capsule 中心到地面 1 + 余量）
    public float groundCheck = 1.1f;
    // 留空就自动从子物体里找
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

    // 向脚下发射一条短线，确认是否站在地面上（忽略 Is Trigger 区域）
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheck, ~0, QueryTriggerInteraction.Ignore);
    }
}
