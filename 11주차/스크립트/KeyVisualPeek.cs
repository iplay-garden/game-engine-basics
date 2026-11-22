using UnityEngine;
using UnityEngine.InputSystem;

// 키 비주얼과 지금 화면을 비교하는 점검 도구
// ▶ Play 중 K 키: 꺼짐 → 오른쪽 위 작게 → 반투명 겹치기 → 꺼짐
// 붙일 곳: 아무 오브젝트 (예: LookTools) · Key Visual 칸에 KeyVisual_01.png를 끌어 놓기
// 스크린샷(P 키)은 이 그림을 끈 상태에서 찍는다
// 점검용이므로 기말 빌드 전에는 이 컴포넌트를 끄거나 지운다
public class KeyVisualPeek : MonoBehaviour
{
    // 비교할 그림 (8주차에 만든 키 비주얼)
    public Texture keyVisual;
    // 누를 키
    public Key key = Key.K;
    // 오른쪽 위 그림의 크기 (화면 너비에 대한 비율)
    [Range(0.15f, 0.5f)] public float cornerSize = 0.3f;
    // 겹치기 때의 진하기 (0 = 투명, 1 = 불투명)
    [Range(0.1f, 0.9f)] public float overlayAlpha = 0.5f;

    // 0 = 꺼짐, 1 = 오른쪽 위, 2 = 겹치기
    int mode = 0;

    void Update()
    {
        if (Keyboard.current == null) return;
        if (Keyboard.current[key].wasPressedThisFrame) mode = (mode + 1) % 3;
    }

    // OnGUI: 화면 맨 위에 그림을 직접 그린다 (Canvas가 필요 없다)
    void OnGUI()
    {
        if (keyVisual == null || mode == 0) return;

        if (mode == 1)
        {
            float w = Screen.width * cornerSize;
            float h = w * keyVisual.height / keyVisual.width;
            Rect r = new Rect(Screen.width - w - 12f, 12f, w, h);
            GUI.color = Color.white;
            // 흰 테두리 → 그림
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
