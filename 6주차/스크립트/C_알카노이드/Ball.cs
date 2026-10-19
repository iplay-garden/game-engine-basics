using UnityEngine;
using UnityEngine.InputSystem;

// 알카노이드 공: Space로 발사, 늘 같은 속도로 튕긴다
// 붙일 곳: 공 Sphere (Tag = Player)
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public float speed = 8f;
    // 발사 전 공이 따라다닐 패들
    public Transform paddle;
    public Vector3 offset = new Vector3(0f, 0f, 0.6f);
    public AudioClip bounceClip;

    Rigidbody rb;
    bool launched;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezePositionY
                       | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (launched || !GameManager.IsPlaying) return;

        // 발사 전에는 패들 위에 붙어 있다
        transform.position = paddle.position + offset;

        Keyboard kb = Keyboard.current;
        if (kb != null && kb.spaceKey.wasPressedThisFrame)
        {
            launched = true;
            rb.isKinematic = false;
            rb.linearVelocity = new Vector3(0.5f, 0f, 1f).normalized * speed;
        }
    }

    void FixedUpdate()
    {
        if (!launched) return;

        Vector3 v = rb.linearVelocity;
        v.y = 0f;
        // 옆으로만 오가며 끝나지 않는 것을 막는다
        float minZ = 0.3f;
        if (v.sqrMagnitude < 0.01f) v = Vector3.forward;
        v = v.normalized;
        if (Mathf.Abs(v.z) < minZ) v.z = v.z >= 0f ? minZ : -minZ;
        rb.linearVelocity = v.normalized * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.PlaySfx(bounceClip);

        // 패들의 어디에 맞았는지로 튕기는 각도를 바꾼다
        if (collision.gameObject.GetComponent<PaddleMove>() != null)
        {
            Transform p = collision.transform;
            float half = p.localScale.x * 0.5f;
            float x = (transform.position.x - p.position.x) / half;
            x = Mathf.Clamp(x, -1f, 1f);
            rb.linearVelocity = new Vector3(x, 0f, 1f).normalized * speed;
        }
    }
}
