using UnityEngine;

// 弹柱：球碰到就加分并把球用力弹开
// 挂载位置：弹柱 Cylinder
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
