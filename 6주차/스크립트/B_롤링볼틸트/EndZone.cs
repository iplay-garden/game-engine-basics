using UnityEngine;

// 공통: 공(Player 태그)이 들어오면 게임을 끝내는 구역
// 골인 지점이면 Is Win 체크, 떨어지는 구역이면 비움
// 붙일 곳: Is Trigger를 켠 Collider가 있는 물체
public class EndZone : MonoBehaviour
{
    public bool isWin = false;
    // 성공 때 남은 시간 1초당 보너스 점수
    public int bonusPerSecond = 10;
    public AudioClip clip;
    public GameObject effect;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (isWin)
        {
            gm.AddScore(Mathf.CeilToInt(gm.TimeLeft) * bonusPerSecond);
        }
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, other.transform.position);
        gm.EndGame(isWin);
    }
}
