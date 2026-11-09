using UnityEngine;

// 애니메이션 연결 ①: 플레이어의 움직임 · 승패를 Animator에 알려 준다
// 붙일 곳: Player (Rigidbody가 있는 오브젝트)
// Animator 파라미터: Speed (Float) · Win (Trigger) · Lose (Trigger)
public class PlayerAnimator : MonoBehaviour
{
    // 애니메이션을 재생할 Animator (Robot · 모델 쪽). 비우면 자식에서 자동으로 찾는다
    public Animator animator;

    Rigidbody rb;
    bool wasPlaying;
    bool ended;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        bool playing = GameManager.IsPlaying;

        // 땅 위에서 움직이는 빠르기 (위아래 속도는 빼고 계산)
        Vector3 v = rb != null ? rb.linearVelocity : Vector3.zero;
        v.y = 0f;
        animator.SetFloat("Speed", playing ? v.magnitude : 0f);

        // 플레이 중 → 끝남 으로 바뀐 순간 한 번만: 성공이면 Win, 실패면 Lose
        if (wasPlaying && !playing && !ended)
        {
            ended = true;
            GameManager gm = GameManager.Instance;
            // 결과 화면 제목이 성공 문구(CLEAR!)와 같으면 성공
            bool win = gm != null && gm.resultTitle != null && gm.resultTitle.text == gm.winMessage;
            animator.SetTrigger(win ? "Win" : "Lose");
        }
        wasPlaying = playing;
    }
}
