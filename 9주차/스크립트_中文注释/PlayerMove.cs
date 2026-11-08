using UnityEngine;
using UnityEngine.InputSystem;

// 玩家移动（WASD · 方向键）— 用于俯视角 3D 游戏
// 挂载位置：玩家（需要 Rigidbody + Collider）
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    // 每秒移动速度
    public float speed = 5f;
    // 朝向移动方向的模型（子物体，可留空）
    public Transform model;

    Rigidbody rb;
    Vector3 input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 锁定旋转，碰撞时不会倒下
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        float h = 0f, v = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) h -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) h += 1f;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) v += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) v -= 1f;
        input = new Vector3(h, 0f, v).normalized;

        // 让模型转向移动方向
        if (model != null && input != Vector3.zero) model.forward = input;
    }

    void FixedUpdate()
    {
        Vector3 vel = input * speed;
        vel.y = rb.linearVelocity.y;   // 保留下落速度
        rb.linearVelocity = vel;
    }
}
