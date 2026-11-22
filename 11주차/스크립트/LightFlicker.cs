using UnityEngine;

// (도전) 횃불 · 촛불처럼 빛의 세기가 흔들린다
// 붙일 곳: Point Light (같은 오브젝트의 Light를 자동으로 찾는다)
// Intensity는 Inspector에 넣은 값을 기준으로 위아래로 흔들린다
[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    // 흔들림의 크기 (0 = 안 흔들림, 0.25 = 기준의 75% ~ 125%)
    [Range(0f, 1f)] public float amount = 0.25f;
    // 흔들리는 빠르기
    public float speed = 6f;

    Light lamp;
    float baseIntensity;   // 처음 세기
    float seed;            // 불마다 박자를 다르게

    void Start()
    {
        lamp = GetComponent<Light>();
        baseIntensity = lamp.intensity;
        seed = Random.Range(0f, 100f);
    }

    void Update()
    {
        // PerlinNoise는 0 ~ 1 사이를 부드럽게 오간다 (Random보다 덜 깜빡인다)
        float n = Mathf.PerlinNoise(seed, Time.time * speed);
        lamp.intensity = baseIntensity * (1f - amount + n * amount * 2f);
    }
}
