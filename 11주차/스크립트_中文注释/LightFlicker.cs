using UnityEngine;

// （挑战）像火把 · 蜡烛一样，光的强度会摇晃
// 挂载位置：Point Light（自动找同一物体上的 Light）
// 以 Inspector 里填的 Intensity 为基准上下摇晃
[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    // 摇晃幅度（0 = 不摇晃，0.25 = 基准的 75% ~ 125%）
    [Range(0f, 1f)] public float amount = 0.25f;
    // 摇晃速度
    public float speed = 6f;

    Light lamp;
    float baseIntensity;   // 初始强度
    float seed;            // 让每盏灯的节奏不同

    void Start()
    {
        lamp = GetComponent<Light>();
        baseIntensity = lamp.intensity;
        seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        // PerlinNoise 在 0 ~ 1 之间平滑变化（比 Random 闪得更自然）
        float n = Mathf.PerlinNoise(seed, Time.time * speed);
        lamp.intensity = baseIntensity * (1f - amount + n * amount * 2f);
    }
}
