using UnityEngine;

// （挑战）追击的敌人：慢慢向玩家移动
// 挂载位置：敌人（同时挂 Hazard，碰到即失败）
public class Chaser : MonoBehaviour
{
    // 追踪目标（Player）
    public Transform target;
    // 每秒移动速度（比玩家慢）
    public float speed = 2f;

    void Update()
    {
        if (!GameManager.IsPlaying || target == null) return;

        Vector3 goal = target.position;
        goal.y = transform.position.y;   // 高度保持不变
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        transform.LookAt(goal);
    }
}
