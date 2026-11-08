using UnityEngine;

// 규칙 부품 ③ 떨어뜨리기: 위에서 물체를 계속 떨어뜨리고, 점점 빨라진다
// 붙일 곳: 빈 오브젝트 Spawner (위치 = 떨어지는 영역의 가운데)
public class FallingSpawner : MonoBehaviour
{
    // 떨어뜨릴 프리팹 (Rigidbody + Hazard가 붙은 것)
    public GameObject prefab;
    // 가운데에서 좌우 · 앞뒤로 퍼지는 거리
    public float rangeX = 4f;
    public float rangeZ = 4f;
    // 처음 간격(초) → 가장 짧은 간격(초)
    public float startInterval = 1.2f;
    public float minInterval = 0.3f;
    // 1초마다 간격이 줄어드는 양
    public float speedUp = 0.02f;
    // 떨어진 물체를 지우는 시간(초)
    public float lifeTime = 5f;

    float interval;
    float timer;

    void Start()
    {
        interval = startInterval;
    }

    void Update()
    {
        if (!GameManager.IsPlaying || prefab == null) return;

        timer += Time.deltaTime;
        interval = Mathf.Max(minInterval, interval - speedUp * Time.deltaTime);

        if (timer >= interval)
        {
            timer = 0f;
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-rangeX, rangeX), 0f, Random.Range(-rangeZ, rangeZ));
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
            Destroy(obj, lifeTime);
        }
    }

    // Scene 뷰에 떨어지는 영역을 노란 상자로 보여 준다
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(rangeX * 2f, 0.1f, rangeZ * 2f));
    }
}
