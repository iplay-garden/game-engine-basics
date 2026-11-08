using UnityEngine;

// 규칙 부품 ① 모으기: 플레이어가 닿으면 점수 + 사라짐, 다 모으면 성공
// 붙일 곳: 모을 물체 (Collider의 Is Trigger 체크)
public class Collectible : MonoBehaviour
{
    // 하나 모을 때 점수
    public int score = 10;
    // 다 모으면 성공으로 끝낼지
    public bool winWhenAllCollected = true;
    // 제자리에서 도는 속도 (0이면 안 돎)
    public float spinSpeed = 90f;
    public AudioClip clip;
    public GameObject effect;

    // 남은 개수 (자동으로 센다)
    static int remaining;

    void OnEnable()  { remaining++; }
    void OnDisable() { remaining--; }

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);

        gameObject.SetActive(false);   // 여기서 remaining이 1 줄어든다
        Destroy(gameObject);

        gm.SetInfo("LEFT " + remaining);
        if (winWhenAllCollected && remaining <= 0) gm.EndGame(true);
    }
}
