using UnityEngine;
using UnityEngine.InputSystem;

// 打砖块的球：按 Space 发射，始终以相同速度反弹
// 挂载位置：球 Sphere（Tag = Player）
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public float speed = 8f;
    // 发射前球跟随的挡板
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

        // 发射前粘在挡板上
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
        // 防止球只在左右来回、永远打不完
        float minZ = 0.3f;
        if (v.sqrMagnitude < 0.01f) v = Vector3.forward;
        v = v.normalized;
        if (Mathf.Abs(v.z) < minZ) v.z = v.z >= 0f ? minZ : -minZ;
        rb.linearVelocity = v.normalized * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.PlaySfx(bounceClip);

        // 根据打在挡板的位置改变反弹角度
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
