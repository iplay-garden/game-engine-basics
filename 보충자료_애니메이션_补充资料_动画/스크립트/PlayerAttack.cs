using UnityEngine;
using UnityEngine.InputSystem;

// 애니메이션 연결 ③: J 키 또는 마우스 왼쪽으로 공격 + Attack 애니메이션
// 붙일 곳: Player · 맞는 쪽에는 Hittable
// Animator 파라미터: Attack (Trigger)
public class PlayerAttack : MonoBehaviour
{
    // 비우면 자식에서 자동으로 찾는다
    public Animator animator;
    // 앞으로 얼마나 떨어진 곳을 칠지 · 그 범위의 반지름
    public float range = 1.2f;
    public float radius = 0.8f;
    // 팔을 휘두른 뒤 실제로 맞는 순간까지 (초)
    public float hitDelay = 0.15f;
    // 다음 공격까지 기다리는 시간 (초)
    public float cooldown = 0.5f;

    Transform facing;   // 바라보는 방향 (애니메이션 모델)
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

    // Scene 뷰에서 공격 범위를 빨간 원으로 보여 준다 (Player 선택 시)
    void OnDrawGizmosSelected()
    {
        Transform f = facing != null ? facing : transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + f.forward * range, radius);
    }
}
