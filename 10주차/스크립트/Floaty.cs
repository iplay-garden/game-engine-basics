using UnityEngine;

// (도전) 꾸미기 부품: 제자리에서 위아래로 둥실 떠 있다
// 붙일 곳: 겉모습만 맡는 자식 오브젝트 Model (부모가 아니라 자식에!)
// 부모(Collider · 규칙 스크립트)는 움직이지 않으므로 닿는 위치는 그대로다
public class Floaty : MonoBehaviour
{
    // 위아래로 움직이는 높이 (미터)
    public float height = 0.2f;
    // 1초에 오르내리는 횟수
    public float speed = 1f;

    Vector3 startLocal;   // 처음 위치 (부모 기준)
    float offset;         // 물체마다 박자를 다르게

    void Start()
    {
        startLocal = transform.localPosition;
        offset = Random.Range(0f, 10f);
    }

    void Update()
    {
        // 부모가 돌거나 누워 있어도 항상 '위아래'로 움직이게 한다
        Vector3 basePos = startLocal;
        Transform parent = transform.parent;
        if (parent != null) basePos = parent.TransformPoint(startLocal);

        // Sin은 -1 ~ 1 사이를 부드럽게 오간다
        float t = (Time.time + offset) * speed * 2f * Mathf.PI;
        float y = Mathf.Sin(t) * height;
        transform.position = basePos + Vector3.up * y;
    }
}
