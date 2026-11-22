using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

// 截图：▶ Play 时按 P 键，把 Game 视图保存成图片文件
// 挂载位置：任意物体（例：空物体 LookTools）
// 保存位置：项目文件夹/Screenshots
//   在 Assets 外面，所以 Project 窗口里看不到 → 用资源管理器打开
// 前 · 后截图要在摄像机没动的状态下截，才是同一构图
public class ShotKey : MonoBehaviour
{
    // 要按的键
    public Key key = Key.P;
    // 文件名前缀（例：before、after）
    public string prefix = "shot";
    // 1 = 原画面大小，2 = 放大两倍
    [Range(1, 4)] public int size = 1;

    void Update()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            // Application.dataPath = 项目文件夹/Assets → 往上一级
            string folder = Path.Combine(Application.dataPath, "..", "Screenshots");
            Directory.CreateDirectory(folder);
            string name = prefix + "_" + System.DateTime.Now.ToString("MMdd_HHmmss") + ".png";
            string path = Path.GetFullPath(Path.Combine(folder, name));
            ScreenCapture.CaptureScreenshot(path, size);
            Debug.Log("截图已保存: " + path);
        }
    }
}
