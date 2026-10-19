using System.Collections;
using UnityEngine;

// 두더지를 무작위로 올린다. 시간이 갈수록 빨라진다
// 붙일 곳: 빈 오브젝트 MoleSpawner
public class MoleSpawner : MonoBehaviour
{
    // 두더지들을 전부 끌어다 넣기
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

                // 0(시작) → 1(끝)으로 가며 간격이 줄어든다
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
