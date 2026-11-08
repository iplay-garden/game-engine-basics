using UnityEngine;

// (도전) 쫓아오는 적: 플레이어 쪽으로 천천히 움직인다
// 붙일 곳: 적 (Hazard도 함께 붙이면 닿을 때 실패)
// 적에 Rigidbody를 붙이고 Is Kinematic을 체크하면 밀려 넘어지지 않는다
public class Chaser : MonoBehaviour
{
    // 따라갈 대상 (Player)
    public Transform target;
    // 초당 이동 속도 (플레이어보다 느리게)
    public float speed = 2f;

    void Update()
    {
        if (!GameManager.IsPlaying || target == null) return;

        Vector3 goal = target.position;
        goal.y = transform.position.y;   // 높이는 그대로
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        transform.LookAt(goal);
    }
}
