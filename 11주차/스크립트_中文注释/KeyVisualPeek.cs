using UnityEngine;
using UnityEngine.InputSystem;

// 把主视觉和当前画面放在一起比较的检查工具
// ▶ Play 时按 K 键：关 → 右上角小图 → 半透明叠加 → 关
// 挂载位置：任意物体（例：LookTools）· 把 KeyVisual_01.png 拖到 Key Visual 栏
// 截图（P 键）要在关掉这张图的状态下截
// 这是检查用的，期末打包前请关掉或删除这个组件
public class KeyVisualPeek : MonoBehaviour
{
    // 要比较的图片（第 8 周做的主视觉）
    public Texture keyVisual;
    // 要按的键
    public Key key = Key.K;
    // 右上角图片的大小（相对画面宽度的比例）
    [Range(0.15f, 0.5f)] public float cornerSize = 0.3f;
    // 叠加时的浓度（0 = 透明，1 = 不透明）
    [Range(0.1f, 0.9f)] public float overlayAlpha = 0.5f;

    // 0 = 关，1 = 右上角，2 = 叠加
    int mode = 0;

    void Update()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame) mode = (mode + 1) % 3;
    }

    // OnGUI：直接在画面最上层画图（不需要 Canvas）
    void OnGUI()
    {
        if (keyVisual == null || mode == 0) return;

        if (mode == 1)
        {
            float w = Screen.width * cornerSize;
            float h = w * keyVisual.height / keyVisual.width;
            Rect r = new Rect(Screen.width - w - 12f, 12f, w, h);
            GUI.color = Color.white;
            // 白色边框 → 图片
            GUI.DrawTexture(new Rect(r.x - 3f, r.y - 3f, r.width + 6f, r.height + 6f), Texture2D.whiteTexture);
            GUI.DrawTexture(r, keyVisual, ScaleMode.ScaleToFit);
        }
        else
        {
            GUI.color = new Color(1f, 1f, 1f, overlayAlpha);
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), keyVisual, ScaleMode.ScaleAndCrop);
            GUI.color = Color.white;
        }
    }
}
