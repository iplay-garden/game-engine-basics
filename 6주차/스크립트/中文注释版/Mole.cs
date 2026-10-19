using System.Collections;
using UnityEngine;

// 一只地鼠（金地鼠 · 炸弹也用同一脚本，只改分数）
// 挂载位置：藏在洞下面的每个地鼠 Capsule
public class Mole : MonoBehaviour
{
    public int score = 10;
    // 冒出的高度 · 停留时间
    public float upHeight = 1.5f;
    public float stayTime = 0.8f;
    public AudioClip hitClip;
    public GameObject hitEffect;

    Vector3 downPos;
    bool isUp;

    void Start()
    {
        downPos = transform.position;
    }

    // 由 MoleSpawner 调用
    public void PopUp()
    {
        if (!isUp) StartCoroutine(UpAndDown());
    }

    IEnumerator UpAndDown()
    {
        isUp = true;
        transform.position = downPos + Vector3.up * upHeight;
        yield return new WaitForSeconds(stayTime);
        transform.position = downPos;
        isUp = false;
    }

    // ClickHit 发送的消息
    void OnClicked()
    {
        if (!isUp || !GameManager.IsPlaying) return;

        StopAllCoroutines();
        Vector3 hitPos = transform.position;
        transform.position = downPos;
        isUp = false;

        GameManager gm = GameManager.Instance;
        gm.AddScore(score);
        gm.PlaySfx(hitClip);
        gm.SpawnFx(hitEffect, hitPos);
    }
}
