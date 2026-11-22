using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// 画面效果（后期处理）开关比较：▶ Play 时按 V 键
// 挂载位置：Global Volume（自动找同一物体上的 Volume）
// 标准：关掉效果时游戏也看得清，打开时氛围明显变好，就算成功
public class VolumeToggle : MonoBehaviour
{
    // 要按的键
    public Key key = Key.V;
    // 要开关的 Volume（留空就在同一物体上找）
    public Volume volume;

    void Start()
    {
        if (volume == null) volume = GetComponent<Volume>();
        if (volume == null) Debug.LogWarning("VolumeToggle：没有找到 Volume。请挂到 Global Volume 上。");
    }

    void Update()
    {
        if (volume == null || Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            volume.enabled = !volume.enabled;
        }
    }

    // 关掉时在画面左上角显示
    void OnGUI()
    {
        if (volume == null || volume.enabled) return;
        GUI.Box(new Rect(12f, 12f, 230f, 26f), "Post Processing OFF (V)");
    }
}
