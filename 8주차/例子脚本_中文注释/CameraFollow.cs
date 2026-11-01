using UnityEngine;

// 摄像机保持一定距离跟随玩家
// 挂载位置：Main Camera
public class CameraFollow : MonoBehaviour
{
    // 跟随目标（玩家）
    public Transform target;
    // 目标到摄像机的距离（向上 10，向后 8）
    public Vector3 offset = new Vector3(0f, 10f, -8f);
    // 跟随平滑度（越大跟得越快）
    public float smooth = 5f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 goal = target.position + offset;
        float k = smooth * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, goal, k);
        transform.LookAt(target);
    }
}
