using UnityEngine;

// 설치 칸: 클릭하면 골드를 내고 타워를 세운다
// 붙일 곳: 타워를 세울 수 있는 바닥 타일
public class Tile : MonoBehaviour
{
    public GameObject towerPrefab;
    public int cost = 50;
    // 타일 중심에서 타워를 세울 높이
    public Vector3 offset = new Vector3(0f, 0.7f, 0f);
    public AudioClip buildClip;
    public AudioClip failClip;
    public GameObject buildEffect;

    bool occupied;

    // ClickHit가 보내는 메시지
    void OnClicked()
    {
        if (occupied || !GameManager.IsPlaying) return;

        GameManager gm = GameManager.Instance;
        if (!DefenseGame.Instance.Spend(cost))
        {
            gm.PlaySfx(failClip);
            return;
        }

        occupied = true;
        Vector3 pos = transform.position + offset;
        Instantiate(towerPrefab, pos, Quaternion.identity);
        gm.PlaySfx(buildClip);
        gm.SpawnFx(buildEffect, pos);
    }
}
