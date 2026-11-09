# 보충자료 (애니메이션) · 补充资料（动画）

**캐릭터 애니메이션 — Unity 기능만으로 만들기** · **角色动画 — 只用 Unity 自带功能制作**
(Unity 6.3 LTS · 영문 에디터 · 英文编辑器)

기본 도형으로 만든 로봇에 동작 7개(Idle · Run · Jump · Attack · Pickup · Victory · Death)를 만들고, 키 입력과 승패에 맞춰 재생합니다. 따라 한 뒤 각자의 프로젝트에 응용하세요. 마지막에 **Mixamo(선택)** 로 사람형 캐릭터에 같은 Animator를 쓰는 방법이 있습니다.
给基础几何体做成的机器人制作 7 个动作（Idle · Run · Jump · Attack · Pickup · Victory · Death），并按按键和胜负播放。跟做之后应用到自己的项目里。最后是 **Mixamo（可选）**：把同一个 Animator 用在人形角色上。

## 받을 파일 · 下载文件

| | 한국어 | 简体中文 |
|---|---|---|
| 따라하기 슬라이드 · 跟做幻灯片 | [캐릭터애니메이션_따라하기.pptx](게임엔진기초_보충자료_애니메이션_캐릭터애니메이션_따라하기.pptx) | [角色动画跟做.pptx](游戏引擎基础_补充资料_动画_角色动画跟做.pptx) |
| 예제 스크립트 · 示例脚本 | [스크립트](스크립트) | [스크립트_中文注释](스크립트_中文注释) |

## 예제 스크립트 · 示例脚本

| 스크립트 | 하는 일 · 作用 | 붙일 곳 · 挂载位置 |
|---|---|---|
| PlayerAnimator | 이동 속도 → Speed · 결과 → Win / Lose · 移动速度 → Speed · 结果 → Win / Lose | Player |
| PlayerJump | Space 점프 · 按 Space 跳跃 | Player |
| PlayerAttack | J · 마우스 왼쪽 공격 · 按 J · 鼠标左键攻击 | Player |
| Hittable | 맞으면 점수 + 사라짐 · 被打就得分 + 消失 | 적 · 敌人 |
| PickupItem | 가까이 가서 E로 줍기 · 走近按 E 捡起 | 주울 물건 · 可捡物品 |
| Editor/RobotExampleBuilder (선택 · 可选) | 로봇 · 클립 7개 · Animator 완성본을 한 번에 생성 (비교용) · 一次生成机器人 · 7 个片段 · Animator 完成版（用于对比） | `Assets/_Project/Editor` 폴더 · 文件夹 |

- 9주차 스크립트(GameManager · PlayerMove)와 함께 씁니다. · 与第 9 周的脚本（GameManager · PlayerMove）一起使用。
- 먼저 Play 씬을 복제한 **연습 씬(AnimPractice)** 에서 따라 하세요. · 请先在复制 Play 场景得到的**练习场景（AnimPractice）**里跟做。
