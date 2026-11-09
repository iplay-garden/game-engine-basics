using UnityEngine;

// 애니메이션 연결 ③-2: 공격을 맞는 물체 (적 · 상자 등)
// 붙일 곳: 맞을 물체 (Collider 필요) — Chaser 적에 붙이면 '때려서 없애기'
public class Hittable : MonoBehaviour
{
    // 몇 번 맞으면 사라지는지
    public int hp = 1;
    // 사라질 때 점수
    public int score = 20;
    public AudioClip clip;
    public GameObject effect;

    bool dead;

    // PlayerAttack이 부른다
    public void TakeHit()
    {
        if (dead || !GameManager.IsPlaying) return;

        hp--;
        if (hp > 0) return;

        dead = true;
        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(clip);
        gm.SpawnFx(effect, transform.position);
        Destroy(gameObject);
    }
}
