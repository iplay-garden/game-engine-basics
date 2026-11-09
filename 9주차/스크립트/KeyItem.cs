using UnityEngine;

// 규칙 부품 ④-1 열쇠: 플레이어가 닿으면 열쇠를 얻는다
// 붙일 곳: 열쇠 (Collider의 Is Trigger 체크)
public class KeyItem : MonoBehaviour
{
    // 열쇠를 가졌는지 (GoalDoor가 확인)
    public static bool HasKey;
    public int score = 50;
    public AudioClip clip;

    void Start()
    {
        HasKey = false;   // 다시 시작할 때 초기화
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        HasKey = true;
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SetInfo("GO TO THE DOOR!");
        gameObject.SetActive(false);
    }
}
