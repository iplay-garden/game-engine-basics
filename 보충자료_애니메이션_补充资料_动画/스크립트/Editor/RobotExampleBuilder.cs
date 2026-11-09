using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

// (선택) 완성 예제 자동 생성 — 따라하기 자료와 똑같은 로봇 · 클립 7개 · Animator Controller를 한 번에 만든다
// 놓을 곳: Assets/_Project/Editor 폴더 (폴더 이름이 꼭 Editor)
// 쓰는 법: Hierarchy에서 Player를 선택 → 메뉴 Tools > Animation Example > Build Robot
// 결과: Player 아래 RobotExample · Assets/_Project/Animations/RobotExample 폴더
public static class RobotExampleBuilder
{
    [MenuItem("Tools/Animation Example/Build Robot")]
    static void Build()
    {
        Transform parent = Selection.activeTransform;

        // 저장할 폴더 (이미 있으면 RobotExample 1, 2 … 로 새로 만든다)
        if (!AssetDatabase.IsValidFolder("Assets/_Project")) AssetDatabase.CreateFolder("Assets", "_Project");
        if (!AssetDatabase.IsValidFolder("Assets/_Project/Animations")) AssetDatabase.CreateFolder("Assets/_Project", "Animations");
        string folder = AssetDatabase.GenerateUniqueAssetPath("Assets/_Project/Animations/RobotExample");
        AssetDatabase.CreateFolder("Assets/_Project/Animations", Path.GetFileName(folder));

        // 재질: 파랑 = 플레이어
        Material mat = MakeMat("M_Robot", new Color(0.25f, 0.45f, 0.95f), folder);
        Material eyeMat = MakeMat("M_RobotEye", new Color(0.95f, 0.95f, 0.95f), folder);

        // 로봇 부품 (자료의 부품 표와 같음)
        GameObject root = new GameObject("RobotExample");
        Undo.RegisterCreatedObjectUndo(root, "Build Robot");
        Transform Robot = root.transform;
        if (parent != null)
        {
            Robot.SetParent(parent, false);
            Robot.localPosition = new Vector3(0f, -1f, 0f);   // Capsule 가운데 → 발바닥
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

        // 애니메이션 클립 7개 (자료의 키 표와 같음)
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
        Debug.Log("RobotExample 완성 — PlayerMove의 Model 칸에 RobotExample을 넣고, 원래 모델은 꺼 두세요");
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
        Object.DestroyImmediate(go.GetComponent<Collider>());   // 부품의 Collider는 지운다 (몸통 판정은 Player의 Capsule)
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

    // times와 values를 번갈아 적은 배열 → 회전 곡선 하나
    static void Curve(AnimationClip clip, string path, string axis, float[] tv)
    {
        // 회전은 x · y · z 세 축을 모두 키로 넣는다 (Inspector에서 녹화할 때와 같음). 나머지 축은 0
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
