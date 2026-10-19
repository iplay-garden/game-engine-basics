using UnityEngine;
using UnityEngine.InputSystem;

// 配方 D 第6周版：有 N 次机会，发射几秒后回到原位
// 挂载位置：发射物（用此内容替换第5周的文件）
[RequireComponent(typeof(Rigidbody))]
public class LaunchForce : MonoBehaviour
{
    public float maxPower = 15f;
    public float chargeTime = 1.5f;
    public Vector3 direction = new Vector3(1f, 1f, 0f);
    // 发射次数 · 发射后回到原位的时间（秒）
    public int shots = 3;
    public float resetDelay = 3f;
    public AudioClip launchClip;

    public int ShotsLeft { get; private set; }

    Rigidbody rb;
    Vector3 startPos;
    Quaternion startRot;
    float charge;
    bool launched;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;
        startRot = transform.rotation;
        ShotsLeft = shots;
        ShowInfo();
    }

    void Update()
    {
        if (!GameManager.IsPlaying || launched) return;

        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.spaceKey.isPressed)
        {
            charge = Mathf.Min(charge + Time.deltaTime, chargeTime);
            ShowInfo();
        }

        if (kb.spaceKey.wasReleasedThisFrame)
        {
            launched = true;
            ShotsLeft--;
            rb.isKinematic = false;
            float power = maxPower * (charge / chargeTime);
            rb.AddForce(direction.normalized * power, ForceMode.Impulse);
            GameManager.Instance.PlaySfx(launchClip);
            charge = 0f;
            ShowInfo();
            Invoke(nameof(ResetShot), resetDelay);
        }
    }

    void ResetShot()
    {
        if (!GameManager.IsPlaying) return;

        // 次数用完而王冠还在 → 失败
        if (ShotsLeft <= 0)
        {
            GameManager.Instance.EndGame(false);
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetPositionAndRotation(startPos, startRot);
        launched = false;
    }

    void ShowInfo()
    {
        int percent = Mathf.RoundToInt(charge / chargeTime * 100f);
        string info = "SHOTS " + ShotsLeft + "   POWER " + percent + "%";
        GameManager.Instance.SetInfo(info);
    }
}
