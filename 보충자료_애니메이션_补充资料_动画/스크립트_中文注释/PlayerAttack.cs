using UnityEngine;
using UnityEngine.InputSystem;

// 动画连接 ③：按 J 键或鼠标左键攻击 + Attack 动画
// 挂载位置：Player · 被打的一方挂 Hittable
// Animator 参数：Attack (Trigger)
public class PlayerAttack : MonoBehaviour
{
    // 留空就自动从子物体里找
    public Animator animator;
    // 打前方多远的地方 · 该范围的半径
    public float range = 1.2f;
    public float radius = 0.8f;
    // 挥臂之后到真正打中的时间（秒）
    public float hitDelay = 0.15f;
    // 到下一次攻击要等的时间（秒）
    public float cooldown = 0.5f;

    Transform facing;   // 面朝的方向（动画模型）
    float nextTime;

    void Start()
    {
        if (animator == null) animator = GetComponentInChildren<Animator>();
        facing = animator != null ? animator.transform : transform;
    }

    void Update()
    {
        if (!GameManager.IsPlaying) return;

        Keyboard kb = Keyboard.current;
        Mouse mouse = Mouse.current;
        bool pressed = (kb != null && kb.jKey.wasPressedThisFrame)
                    || (mouse != null && mouse.leftButton.wasPressedThisFrame);
        if (!pressed || Time.time < nextTime) return;

        nextTime = Time.time + cooldown;
        if (animator != null) animator.SetTrigger("Attack");
        Invoke(nameof(Hit), hitDelay);
    }

    void Hit()
    {
        if (!GameManager.IsPlaying) return;

        Vector3 center = transform.position + facing.forward * range;
        Collider[] hits = Physics.OverlapSphere(center, radius);
        foreach (Collider c in hits)
        {
            Hittable target = c.GetComponentInParent<Hittable>();
            if (target != null) target.TakeHit();
        }
    }

    // 在 Scene 视图里用红色圆显示攻击范围（选中 Player 时）
    void OnDrawGizmosSelected()
    {
        Transform f = facing != null ? facing : transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + f.forward * range, radius);
    }
}
