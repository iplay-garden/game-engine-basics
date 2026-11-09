using UnityEngine;
using UnityEngine.InputSystem;

// 动画连接 ④：走近后按 E 键捡起 + Pickup 动画
// 挂载位置：要捡的物体（勾选 Collider 的 Is Trigger · 范围稍微大一点）
// Animator 参数：Pickup (Trigger) — 玩家那边的 Animator
public class PickupItem : MonoBehaviour
{
    public int score = 10;
    // 走近时在画面上显示的文字
    public string message = "PRESS E";
    // 显示捡起动作后到物体消失的时间（秒）
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
