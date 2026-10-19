using System.Collections;
using UnityEngine;

// 웨이브마다 적을 만든다. 마지막 웨이브를 다 막으면 성공
// 붙일 곳: 빈 오브젝트 WaveSpawner
public class WaveSpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    // 길 순서대로: 0번 = 출발, 마지막 = 기지
    public Transform[] waypoints;
    public int[] enemiesPerWave = { 5, 10, 15 };
    public float spawnInterval = 1f;
    public float waveDelay = 4f;
    // 웨이브마다 빨라지는 정도
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

            // 이번 웨이브의 적이 모두 사라질 때까지 기다린다
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
