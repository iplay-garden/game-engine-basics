using System.Collections;
using UnityEngine;

// 每波生成敌人，挡住最后一波即成功
// 挂载位置：空物体 WaveSpawner
public class WaveSpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    // 按路线顺序：0 号 = 出发点，最后 = 基地
    public Transform[] waypoints;
    public int[] enemiesPerWave = { 5, 10, 15 };
    public float spawnInterval = 1f;
    public float waveDelay = 4f;
    // 每波加快的程度
    public float speedUpPerWave = 0.3f;
    public AudioClip waveClip;

    IEnumerator Start()
    {
        while (!GameManager.IsPlaying) yield return null;

        GameManager gm = GameManager.Instance;
        int total = enemiesPerWave.Length;

        for (int w = 0; w < total; w++)
        {
            DefenseGame.Instance.SetWave(w + 1, total);
            yield return new WaitForSeconds(waveDelay);
            gm.PlaySfx(waveClip);

            for (int i = 0; i < enemiesPerWave[w]; i++)
            {
                if (!GameManager.IsPlaying) yield break;
                Enemy enemy = Instantiate(enemyPrefab);
                enemy.speed += w * speedUpPerWave;
                enemy.Init(waypoints);
                yield return new WaitForSeconds(spawnInterval);
            }

            // 等待本波敌人全部消失
            while (Enemy.Alive > 0)
            {
                if (!GameManager.IsPlaying) yield break;
                yield return null;
            }
        }

        if (GameManager.IsPlaying)
        {
            gm.AddScore(DefenseGame.Instance.baseHp * 100);
            gm.EndGame(true);
        }
    }
}
