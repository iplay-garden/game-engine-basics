using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// 화면 효과(포스트 프로세싱) 켜고 끄기 비교: ▶ Play 중 V 키
// 붙일 곳: Global Volume (같은 오브젝트의 Volume을 자동으로 찾는다)
// 기준: 효과를 껐을 때도 게임이 잘 보이고, 켰을 때 분위기가 분명히 좋아지면 성공
public class VolumeToggle : MonoBehaviour
{
    // 누를 키
    public Key key = Key.V;
    // 켜고 끌 Volume (비워 두면 같은 오브젝트에서 찾는다)
    public Volume volume;

    void Start()
    {
        if (volume == null) volume = GetComponent<Volume>();
        if (volume == null) Debug.LogWarning("VolumeToggle: Volume을 찾지 못했습니다. Global Volume에 붙이세요.");
    }

    void Update()
    {
        if (volume == null || Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            volume.enabled = !volume.enabled;
        }
    }

    // 꺼져 있을 때 화면 왼쪽 위에 표시
    void OnGUI()
    {
        if (volume == null || volume.enabled) return;
        GUI.Box(new Rect(12f, 12f, 230f, 26f), "Post Processing OFF (V)");
    }
}
