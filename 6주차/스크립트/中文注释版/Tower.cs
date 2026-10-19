using UnityEngine;

// 塔：按固定间隔攻击射程内最近的敌人
// 挂载位置：塔预制体（Cylinder）
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

    // 在 Scene 视图中用黄色圆显示射程
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
