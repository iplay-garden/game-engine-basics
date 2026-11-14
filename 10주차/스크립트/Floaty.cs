using UnityEngine;

// (도전) 꾸미기 부품: 제자리에서 위아래로 둥실 떠 있고, 천천히 돈다
// 붙일 곳: 겉모습만 맡는 자식 오브젝트 Model (부모가 아니라 자식에!)
// 부모(Collider · 규칙 스크립트)는 움직이지 않으므로 닿는 위치는 그대로다
public class Floaty : MonoBehaviour
{
    // 위아래로 움직이는 높이 (미터)
    public float height = 0.2f;
    // 1초에 오르내리는 횟수
    public float speed = 1f;
    // 초당 도는 각도 (0이면 돌지 않음)
    public float spinSpeed = 0f;

    Vector3 startPos;   // 처음 위치 (부모 기준)
    float offset;       // 물체마다 박자를 다르게

    void Start()
    {
        startPos = transform.localPosition;
        offset = Random.Range(0f, 10f);
    }

    void Update()
    {
        // Sin은 -1 ~ 1 사이를 부드럽게 오간다 → 위아래 움직임
        float t = (Time.time + offset) * speed * 2f * Mathf.PI;
        float y = Mathf.Sin(t) * height;
        transform.localPosition = startPos + new Vector3(0f, y, 0f);
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }
}
