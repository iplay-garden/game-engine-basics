using System.Collections;
using UnityEngine;

// 随机让地鼠冒出，越到后面越快
// 挂载位置：空物体 MoleSpawner
public class MoleSpawner : MonoBehaviour
{
    // 把所有地鼠拖进来
    public Mole[] moles;
    public float startInterval = 1.2f;
    public float endInterval = 0.6f;

    IEnumerator Start()
    {
        while (true)
        {
            GameManager gm = GameManager.Instance;
            float interval = startInterval;

            if (GameManager.IsPlaying && moles.Length > 0)
            {
                Mole mole = moles[Random.Range(0, moles.Length)];
                mole.PopUp();

                // 从 0（开始）到 1（结束），间隔逐渐缩短
                if (gm.timeLimit > 0f)
                {
                    float progress = 1f - gm.TimeLeft / gm.timeLimit;
                    interval = Mathf.Lerp(startInterval, endInterval, progress);
                }
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
