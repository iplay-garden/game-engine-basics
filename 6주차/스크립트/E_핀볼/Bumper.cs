using UnityEngine;

// 범퍼: 공이 닿으면 점수 + 공을 세게 밀어낸다
// 붙일 곳: 범퍼 Cylinder
public class Bumper : MonoBehaviour
{
    public int score = 100;
    public float pushForce = 8f;
    public AudioClip hitClip;
    public GameObject hitEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!GameManager.IsPlaying) return;

        Rigidbody ball = collision.rigidbody;
        Vector3 dir = collision.transform.position - transform.position;
        dir.y = 0f;
        ball.AddForce(dir.normalized * pushForce, ForceMode.Impulse);

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(hitClip);
        gm.SpawnFx(hitEffect, collision.transform.position);
    }
}
