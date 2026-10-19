using System.Collections;
using UnityEngine;

// 두더지 하나 (황금 두더지 · 폭탄도 같은 스크립트, 점수만 다르게)
// 붙일 곳: 구멍 아래에 숨겨 둔 각 두더지 Capsule
public class Mole : MonoBehaviour
{
    public int score = 10;
    // 올라오는 높이 · 올라와 있는 시간
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

    // MoleSpawner가 부른다
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

    // ClickHit가 보내는 메시지
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
