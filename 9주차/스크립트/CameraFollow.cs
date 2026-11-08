using UnityEngine;

// 카메라가 플레이어를 일정한 거리에서 따라간다
// 붙일 곳: Main Camera
public class CameraFollow : MonoBehaviour
{
    // 따라갈 대상 (플레이어)
    public Transform target;
    // 대상에서 카메라까지의 거리 (위로 10, 뒤로 8)
    public Vector3 offset = new Vector3(0f, 10f, -8f);
    // 따라가는 부드러움 (클수록 빨리 붙는다)
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
