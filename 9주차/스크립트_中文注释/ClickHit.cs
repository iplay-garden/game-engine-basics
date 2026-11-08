using UnityEngine;
using UnityEngine.InputSystem;

// 配方 A 第6周版：向被点击的物体发送 OnClicked 消息
// 挂载位置：Main Camera（用此内容替换第5周的文件）
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
                // 被点中的物体如有 OnClicked 就执行（Mole · Tile）
                hit.collider.SendMessage("OnClicked",
                    SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
