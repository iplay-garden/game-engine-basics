using UnityEngine;

// 규칙 부품 ② 피하기: 플레이어가 닿으면 실패
// 붙일 곳: 장애물 · 떨어지는 물체 · 적 (Collider 필요)
public class Hazard : MonoBehaviour
{
    public AudioClip clip;
    public GameObject effect;

    // 단단한 물체끼리 부딪힐 때
    void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    // Is Trigger를 켠 경우
    void OnTriggerEnter(Collider other)
    {
        Hit(other.gameObject);
    }

    void Hit(GameObject target)
    {
        if (!target.CompareTag("Player") || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, target.transform.position);
        gm.EndGame(false);
    }
}
