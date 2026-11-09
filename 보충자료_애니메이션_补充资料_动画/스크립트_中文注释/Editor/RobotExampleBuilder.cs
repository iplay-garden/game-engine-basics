using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

// （可选）自动生成完成示例 — 一次做出和跟做资料完全一样的机器人 · 7 个动画片段 · Animator Controller
// 放置位置：Assets/_Project/Editor 文件夹（文件夹名必须是 Editor）
// 用法：在 Hierarchy 选中 Player → 菜单 Tools > Animation Example > Build Robot
// 结果：Player 下生成 RobotExample · 以及 Assets/_Project/Animations/RobotExample 文件夹
public static class RobotExampleBuilder
{
    [MenuItem("Tools/Animation Example/Build Robot")]
    static void Build()
    {
        Transform parent = Selection.activeTransform;

        // 保存的文件夹（已存在就新建 RobotExample 1, 2 …）
        if (!AssetDatabase.IsValidFolder("Assets/_Project")) AssetDatabase.CreateFolder("Assets", "_Project");
        if (!AssetDatabase.IsValidFolder("Assets/_Project/Animations")) AssetDatabase.CreateFolder("Assets/_Project", "Animations");
        string folder = AssetDatabase.GenerateUniqueAssetPath("Assets/_Project/Animations/RobotExample");
        AssetDatabase.CreateFolder("Assets/_Project/Animations", Path.GetFileName(folder));

        // 材质：蓝色 = 玩家
        Material mat = MakeMat("M_Robot", new Color(0.25f, 0.45f, 0.95f), folder);
        Material eyeMat = MakeMat("M_RobotEye", new Color(0.95f, 0.95f, 0.95f), folder);

        // 机器人部件（和资料里的部件表相同）
        GameObject root = new GameObject("RobotExample");
        Undo.RegisterCreatedObjectUndo(root, "Build Robot");
        Transform Robot = root.transform;
        if (parent != null)
        {
            Robot.SetParent(parent, false);
            Robot.localPosition = new Vector3(0f, -1f, 0f);   // 从 Capsule 中心 → 脚底
        }
        Transform Pivot = Empty("Pivot", Robot, new Vector3(0f, 0f, 0f));
        Transform Body = Part(PrimitiveType.Cube, "Body", Pivot, new Vector3(0f, 1.1f, 0f), new Vector3(0.6f, 0.7f, 0.4f), mat);
        Transform Head = Part(PrimitiveType.Sphere, "Head", Pivot, new Vector3(0f, 1.7f, 0f), new Vector3(0.45f, 0.45f, 0.45f), mat);
        Transform Eye = Part(PrimitiveType.Cube, "Eye", Head, new Vector3(0f, 0.05f, 0.42f), new Vector3(0.65f, 0.18f, 0.2f), eyeMat);
        Transform ShoulderL = Empty("ShoulderL", Pivot, new Vector3(-0.4f, 1.4f, 0f));
        Transform ArmL = Part(PrimitiveType.Cube, "ArmL", ShoulderL, new Vector3(0f, -0.3f, 0f), new Vector3(0.18f, 0.6f, 0.18f), mat);
        Transform ShoulderR = Empty("ShoulderR", Pivot, new Vector3(0.4f, 1.4f, 0f));
        Transform ArmR = Part(PrimitiveType.Cube, "ArmR", ShoulderR, new Vector3(0f, -0.3f, 0f), new Vector3(0.18f, 0.6f, 0.18f), mat);
        Transform HipL = Empty("HipL", Pivot, new Vector3(-0.15f, 0.75f, 0f));
        Transform LegL = Part(PrimitiveType.Cube, "LegL", HipL, new Vector3(0f, -0.37f, 0f), new Vector3(0.22f, 0.75f, 0.22f), mat);
        Transform HipR = Empty("HipR", Pivot, new Vector3(0.15f, 0.75f, 0f));
        Transform LegR = Part(PrimitiveType.Cube, "LegR", HipR, new Vector3(0f, -0.37f, 0f), new Vector3(0.22f, 0.75f, 0.22f), mat);

        // 7 个动画片段（和资料里的关键帧表相同）
        AnimationClip Idle = NewClip("Idle", true, folder);
        Curve(Idle, "Pivot/ShoulderL", "z", new float[] { 0.0000f, 0f, 0.5000f, -8f, 1.0000f, 0f });
        Curve(Idle, "Pivot/ShoulderR", "z", new float[] { 0.0000f, 0f, 0.5000f, 8f, 1.0000f, 0f });
        Save(Idle, folder);
        AnimationClip Run = NewClip("Run", true, folder);
        Curve(Run, "Pivot/ShoulderL", "x", new float[] { 0.0000f, 40f, 0.2500f, -40f, 0.5000f, 40f });
        Curve(Run, "Pivot/ShoulderR", "x", new float[] { 0.0000f, -40f, 0.2500f, 40f, 0.5000f, -40f });
        Curve(Run, "Pivot/HipL", "x", new float[] { 0.0000f, -30f, 0.2500f, 30f, 0.5000f, -30f });
        Curve(Run, "Pivot/HipR", "x", new float[] { 0.0000f, 30f, 0.2500f, -30f, 0.5000f, 30f });
        Save(Run, folder);
        AnimationClip Jump = NewClip("Jump", false, folder);
        Curve(Jump, "Pivot/HipL", "x", new float[] { 0.0000f, 0f, 0.1500f, -60f, 0.5000f, 0f });
        Curve(Jump, "Pivot/HipR", "x", new float[] { 0.0000f, 0f, 0.1500f, -60f, 0.5000f, 0f });
        Curve(Jump, "Pivot/ShoulderL", "z", new float[] { 0.0000f, 0f, 0.1500f, -70f, 0.5000f, 0f });
        Curve(Jump, "Pivot/ShoulderR", "z", new float[] { 0.0000f, 0f, 0.1500f, 70f, 0.5000f, 0f });
        Save(Jump, folder);
        AnimationClip Attack = NewClip("Attack", false, folder);
        Curve(Attack, "Pivot/ShoulderR", "x", new float[] { 0.0000f, 0f, 0.1333f, -160f, 0.2667f, -40f, 0.4000f, 0f });
        Save(Attack, folder);
        AnimationClip Pickup = NewClip("Pickup", false, folder);
        Curve(Pickup, "Pivot", "x", new float[] { 0.0000f, 0f, 0.2500f, 30f, 0.4000f, 30f, 0.6000f, 0f });
        Curve(Pickup, "Pivot/ShoulderL", "x", new float[] { 0.0000f, 0f, 0.2500f, -70f, 0.4000f, -70f, 0.6000f, 0f });
        Curve(Pickup, "Pivot/ShoulderR", "x", new float[] { 0.0000f, 0f, 0.2500f, -70f, 0.4000f, -70f, 0.6000f, 0f });
        Save(Pickup, folder);
        AnimationClip Victory = NewClip("Victory", false, folder);
        Curve(Victory, "Pivot/ShoulderL", "z", new float[] { 0.0000f, 0f, 0.2500f, -160f, 0.5000f, -140f, 0.7500f, -160f, 1.0000f, -160f });
        Curve(Victory, "Pivot/ShoulderR", "z", new float[] { 0.0000f, 0f, 0.2500f, 160f, 0.5000f, 140f, 0.7500f, 160f, 1.0000f, 160f });
        Save(Victory, folder);
        AnimationClip Death = NewClip("Death", false, folder);
        Curve(Death, "Pivot", "x", new float[] { 0.0000f, 0f, 0.5000f, 90f, 0.8000f, 90f });
        Curve(Death, "Pivot/ShoulderL", "z", new float[] { 0.0000f, 0f, 0.5000f, -30f });
        Curve(Death, "Pivot/ShoulderR", "z", new float[] { 0.0000f, 0f, 0.5000f, 30f });
        Save(Death, folder);

        // Animator Controller
        AnimatorController ctrl = AnimatorController.CreateAnimatorControllerAtPath(folder + "/RobotExample.controller");
        ctrl.AddParameter("Speed", AnimatorControllerParameterType.Float);
        ctrl.AddParameter("Jump", AnimatorControllerParameterType.Trigger);
        ctrl.AddParameter("Attack", AnimatorControllerParameterType.Trigger);
        ctrl.AddParameter("Pickup", AnimatorControllerParameterType.Trigger);
        ctrl.AddParameter("Win", AnimatorControllerParameterType.Trigger);
        ctrl.AddParameter("Lose", AnimatorControllerParameterType.Trigger);
        AnimatorStateMachine sm = ctrl.layers[0].stateMachine;
        AnimatorState sIdle = sm.AddState("Idle", new Vector3(300f, 0f, 0f));
        sIdle.motion = Idle;
        AnimatorState sRun = sm.AddState("Run", new Vector3(300f, 120f, 0f));
        sRun.motion = Run;
        AnimatorState sJump = sm.AddState("Jump", new Vector3(560f, -120f, 0f));
        sJump.motion = Jump;
        AnimatorState sAttack = sm.AddState("Attack", new Vector3(560f, 0f, 0f));
        sAttack.motion = Attack;
        AnimatorState sPickup = sm.AddState("Pickup", new Vector3(560f, 120f, 0f));
        sPickup.motion = Pickup;
        AnimatorState sVictory = sm.AddState("Victory", new Vector3(40f, 240f, 0f));
        sVictory.motion = Victory;
        AnimatorState sDeath = sm.AddState("Death", new Vector3(300f, 240f, 0f));
        sDeath.motion = Death;
        sm.defaultState = sIdle;
        AnimatorStateTransition t;
        t = sIdle.AddTransition(sRun); t.hasExitTime = false; t.duration = 0.1f; t.AddCondition(AnimatorConditionMode.Greater, 0.1f, "Speed"); 
        t = sRun.AddTransition(sIdle); t.hasExitTime = false; t.duration = 0.1f; t.AddCondition(AnimatorConditionMode.Less, 0.1f, "Speed"); 
        t = sm.AddAnyStateTransition(sJump); t.hasExitTime = false; t.duration = 0.05f; t.AddCondition(AnimatorConditionMode.If, 0f, "Jump"); t.canTransitionToSelf = false; 
        t = sJump.AddTransition(sIdle); t.hasExitTime = true; t.exitTime = 1f; t.duration = 0.1f; 
        t = sm.AddAnyStateTransition(sAttack); t.hasExitTime = false; t.duration = 0.05f; t.AddCondition(AnimatorConditionMode.If, 0f, "Attack"); t.canTransitionToSelf = false; 
        t = sAttack.AddTransition(sIdle); t.hasExitTime = true; t.exitTime = 1f; t.duration = 0.1f; 
        t = sm.AddAnyStateTransition(sPickup); t.hasExitTime = false; t.duration = 0.05f; t.AddCondition(AnimatorConditionMode.If, 0f, "Pickup"); t.canTransitionToSelf = false; 
        t = sPickup.AddTransition(sIdle); t.hasExitTime = true; t.exitTime = 1f; t.duration = 0.1f; 
        t = sm.AddAnyStateTransition(sVictory); t.hasExitTime = false; t.duration = 0.1f; t.AddCondition(AnimatorConditionMode.If, 0f, "Win"); t.canTransitionToSelf = false; 
        t = sm.AddAnyStateTransition(sDeath); t.hasExitTime = false; t.duration = 0.1f; t.AddCondition(AnimatorConditionMode.If, 0f, "Lose"); t.canTransitionToSelf = false; 

        Animator anim = root.AddComponent<Animator>();
        anim.runtimeAnimatorController = ctrl;
        anim.applyRootMotion = false;

        AssetDatabase.SaveAssets();
        Selection.activeGameObject = root;
        Debug.Log("RobotExample 完成 — 把 RobotExample 拖到 PlayerMove 的 Model 栏，原来的模型先关闭");
    }

    static Transform Empty(string name, Transform parent, Vector3 pos)
    {
        Transform t = new GameObject(name).transform;
        t.SetParent(parent, false);
        t.localPosition = pos;
        return t;
    }

    static Transform Part(PrimitiveType type, string name, Transform parent, Vector3 pos, Vector3 scale, Material mat)
    {
        GameObject go = GameObject.CreatePrimitive(type);
        go.name = name;
        Object.DestroyImmediate(go.GetComponent<Collider>());   // 删除部件的 Collider（身体判定交给 Player 的 Capsule）
        go.transform.SetParent(parent, false);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.GetComponent<Renderer>().sharedMaterial = mat;
        return go.transform;
    }

    static Material MakeMat(string name, Color color, string folder)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        Material m = new Material(shader);
        m.SetColor("_BaseColor", color);
        m.color = color;
        AssetDatabase.CreateAsset(m, folder + "/" + name + ".mat");
        return m;
    }

    static AnimationClip NewClip(string name, bool loop, string folder)
    {
        AnimationClip clip = new AnimationClip();
        clip.name = name;
        clip.frameRate = 60f;
        AnimationClipSettings s = AnimationUtility.GetAnimationClipSettings(clip);
        s.loopTime = loop;
        AnimationUtility.SetAnimationClipSettings(clip, s);
        return clip;
    }

    // 时间和数值交替排列的数组 → 一条旋转曲线
    static void Curve(AnimationClip clip, string path, string axis, float[] tv)
    {
        // 旋转的 x · y · z 三个轴都要打关键帧（和在 Inspector 里录制时一样）。其余轴为 0
        foreach (string a in new string[] { "x", "y", "z" })
        {
            AnimationCurve curve = new AnimationCurve();
            for (int i = 0; i < tv.Length; i += 2) curve.AddKey(tv[i], a == axis ? tv[i + 1] : 0f);
            EditorCurveBinding b = EditorCurveBinding.FloatCurve(path, typeof(Transform), "localEulerAnglesRaw." + a);
            AnimationUtility.SetEditorCurve(clip, b, curve);
        }
    }

    static void Save(AnimationClip clip, string folder)
    {
        AssetDatabase.CreateAsset(clip, folder + "/" + clip.name + ".anim");
    }
}
