using UnityEngine;

// 타워: 사거리 안의 가장 가까운 적을 일정 간격으로 공격
// 붙일 곳: 타워 프리팹 (Cylinder)
public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireInterval = 0.8f;
    public int damage = 1;
    public AudioClip shotClip;
    public GameObject hitEffect;

    float timer;

    void Update()
    {
        if (!GameManager.IsPlaying) return;

        timer += Time.deltaTime;
        if (timer < fireInterval) return;

        Enemy target = FindNearest();
        if (target == null) return;

        timer = 0f;
        GameManager gm = GameManager.Instance;
        gm.PlaySfx(shotClip);
        gm.SpawnFx(hitEffect, target.transform.position);
        target.TakeDamage(damage);
    }

    Enemy FindNearest()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        Enemy best = null;
        float bestDist = float.MaxValue;

        foreach (Collider c in hits)
        {
            Enemy e = c.GetComponent<Enemy>();
            if (e == null) continue;
            Vector3 diff = e.transform.position - transform.position;
            float d = diff.sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = e;
            }
        }
        return best;
    }

    // Scene 뷰에서 사거리를 노란 원으로 보여 준다
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
