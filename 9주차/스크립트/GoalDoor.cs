using UnityEngine;

// 규칙 부품 ④-2 문: 열쇠를 가지고 닿으면 성공
// 붙일 곳: 문 · 출구 (Collider의 Is Trigger 체크)
public class GoalDoor : MonoBehaviour
{
    // 남은 시간 1초당 보너스 점수
    public int bonusPerSecond = 10;
    public AudioClip clip;
    public GameObject effect;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (!KeyItem.HasKey)
        {
            gm.SetInfo("FIND THE KEY FIRST");
            return;
        }
        gm.AddScore(Mathf.CeilToInt(gm.TimeLeft) * bonusPerSecond);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);
        gm.EndGame(true);
    }
}
