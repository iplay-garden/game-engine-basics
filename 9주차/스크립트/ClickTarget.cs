using UnityEngine;

// 규칙 부품 ④ 맞히기: 클릭하면 점수, 다른 곳으로 순간 이동
// 붙일 곳: 과녁 (Collider 필요) · Main Camera에는 ClickHit
public class ClickTarget : MonoBehaviour
{
    public int score = 10;
    // 다시 나타날 범위 (처음 위치 기준)
    public float rangeX = 5f;
    public float rangeY = 2f;
    public AudioClip clip;
    public GameObject effect;

    Vector3 home;

    void Start()
    {
        home = transform.position;
    }

    // ClickHit이 보내는 메시지
    void OnClicked()
    {
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);

        transform.position = home + new Vector3(
            Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0f);
    }
}
