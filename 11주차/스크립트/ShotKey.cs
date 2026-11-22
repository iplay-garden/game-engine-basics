using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

// 화면 사진 찍기: ▶ Play 중 P 키를 누르면 Game 뷰를 그림 파일로 저장한다
// 붙일 곳: 아무 오브젝트 (예: 빈 오브젝트 LookTools)
// 저장 위치: 프로젝트 폴더/Screenshots
//   Assets 밖이라 Project 창에는 안 보인다 → 탐색기에서 연다
// 전 · 후 사진은 카메라를 움직이지 않은 상태에서 찍어야 같은 구도가 된다
public class ShotKey : MonoBehaviour
{
    // 누를 키
    public Key key = Key.P;
    // 파일 이름 앞부분 (예: before, after)
    public string prefix = "shot";
    // 1 = 화면 크기 그대로, 2 = 두 배 크게
    [Range(1, 4)] public int size = 1;

    void Update()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            // Application.dataPath = 프로젝트 폴더/Assets → 한 칸 위로
            string folder = Path.Combine(Application.dataPath, "..", "Screenshots");
            Directory.CreateDirectory(folder);
            string name = prefix + "_" + System.DateTime.Now.ToString("MMdd_HHmmss") + ".png";
            string path = Path.GetFullPath(Path.Combine(folder, name));
            ScreenCapture.CaptureScreenshot(path, size);
            Debug.Log("스크린샷 저장: " + path);
        }
    }
}
