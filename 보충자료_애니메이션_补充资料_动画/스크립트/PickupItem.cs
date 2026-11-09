using UnityEngine;
using UnityEngine.InputSystem;

// 애니메이션 연결 ④: 가까이 가서 E 키로 줍기 + Pickup 애니메이션
// 붙일 곳: 주울 물체 (Collider의 Is Trigger 체크 · 범위는 조금 넉넉하게)
// Animator 파라미터: Pickup (Trigger) — 플레이어 쪽 Animator
public class PickupItem : MonoBehaviour
{
    public int score = 10;
    // 가까이 갔을 때 화면에 띄울 글
    public string message = "PRESS E";
    // 줍는 동작이 보인 뒤 사라질 때까지 (초)
    public float delay = 0.3f;
    public AudioClip clip;
    public GameObject effect;

    bool inRange;
    bool taken;
    Animator playerAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = true;
        playerAnimator = other.GetComponentInChildren<Animator>();
        if (GameManager.IsPlaying && !taken) GameManager.Instance.SetInfo(message);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (GameManager.IsPlaying && !taken) GameManager.Instance.SetInfo("");
    }

    void Update()
    {
        if (!inRange || taken || !GameManager.IsPlaying) return;

        Keyboard kb = Keyboard.current;
        if (kb == null || !kb.eKey.wasPressedThisFrame) return;

        taken = true;
        if (playerAnimator != null) playerAnimator.SetTrigger("Pickup");

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SetInfo("");
        Invoke(nameof(Vanish), delay);
    }

    void Vanish()
    {
        GameManager.Instance.SpawnFx(effect, transform.position);
        Destroy(gameObject);
    }
}
