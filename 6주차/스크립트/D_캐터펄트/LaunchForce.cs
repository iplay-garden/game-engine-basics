using UnityEngine;
using UnityEngine.InputSystem;

// 레시피 D 6주차판: 기회 N번, 쏜 뒤 몇 초 지나면 제자리로
// 붙일 곳: 발사체 (5주차 파일을 이 내용으로 바꾸기)
[RequireComponent(typeof(Rigidbody))]
public class LaunchForce : MonoBehaviour
{
    public float maxPower = 15f;
    public float chargeTime = 1.5f;
    public Vector3 direction = new Vector3(1f, 1f, 0f);
    // 발사 기회 · 쏜 뒤 돌아오기까지 시간(초)
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

        // 기회를 다 썼는데 왕관이 남아 있으면 실패
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
