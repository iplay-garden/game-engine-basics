using UnityEngine;
using UnityEngine.InputSystem;

// 레시피 A 6주차판: 클릭한 물체에 OnClicked 메시지를 보낸다
// 붙일 곳: Main Camera (5주차 파일을 이 내용으로 바꾸기)
public class ClickHit : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.IsPlaying) return;

        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 맞은 물체에 OnClicked가 있으면 실행 (Mole · Tile)
                hit.collider.SendMessage("OnClicked",
                    SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
