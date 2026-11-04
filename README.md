# Unity-ScriptableObject

> ScriptableObject 学习与实战 ｜ Unity 2022.3.62f1 ｜ 8 个 Lesson

按「创建 → 使用 → 非持久数据 → 持久化 → 配置数据 → 数据复用 → 数据驱动多态 → 单例获取」的顺序，把 ScriptableObject 从语法到工程实践走了一遍。

## 为什么要用 ScriptableObject

| 问题 | SO 的解法 |
|---|---|
| 配置写在代码里，改数值要改代码 | 数据变成资产，Inspector 里直接改，改完即时生效 |
| 配置存 JSON/XML，字段改名无提示 | SO 是强类型资产，字段改名编译期报错 |
| 配置里要引用 Prefab / 贴图 / 音频，只能存路径字符串 | SO 可以直接持有 `GameObject`、`Sprite`、`AudioClip` 引用 |
| 多个对象共享同一份数据，各自复制一份 | SO 是引用语义，所有使用者共享同一份内存 |
| 不同数据需要不同行为 | **SO 支持多态**：抽象 SO + 子类实现不同逻辑 |

## Lesson 索引

| Lesson | 主题 | 关键点 |
|---|---|---|
| 1 | 数据文件创建 | `[CreateAssetMenu]` 菜单创建、字段序列化 |
| 2 | 数据文件的使用 | 在面板脚本里引用 SO 资产并读取 |
| 3 | 非持久数据 | 运行期修改 SO 字段会**污染资产**（退出播放模式不还原），这是最容易被忽略的坑 |
| 4 | 让非持久数据持久化 | 用 `ScriptableObject.CreateInstance<T>()` 创建运行期副本，改副本不污染原资产 |
| 5 | 配置数据 | 角色属性表（`RoleInfo`）：数值、资源引用集中管理 |
| 6 | 数据复用 | 多个子弹共享一份 `BulletInfo`，改一处全部生效 |
| 7 | 数据带来的多态行为 | 抽象 SO + 子类，不同数据执行不同逻辑（见下） |
| 8 | 单例模式的获取数据 | 泛型 SO 单例基类，`Resources.Load` 懒加载 |

## 两个最有价值的实现

### 1. 数据驱动的多态（Lesson 7）

传统写法里，「不同音频播放策略」要用 `switch` 或策略类；用 SO 可以把**行为挂在数据上**：

```csharp
public abstract class AudioBase : ScriptableObject
{
    public abstract void Play(AudioSource source);
}

// 子类一：随机播放
public class RandoPlayAudio : AudioBase
{
    public override void Play(AudioSource source) { /* 随机取一段播放 */ }
}
```

派生类各自实现 `Play`，业务代码只持有 `AudioBase`：

```csharp
audioData.Play(audioSource);   // 不需要知道具体是哪种策略
```

新增播放策略只需新建一个 SO 资产类型，业务代码零改动。这是 SO 相比 JSON 配置最大的优势——**数据不仅能存值，还能带行为**。

### 2. 泛型 SO 单例基类（Lesson 8）

```csharp
public class SingScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<T>("ScriptableObject/" + typeof(T).Name);
            if (instance == null)
                instance = ScriptableObject.CreateInstance<T>();   // 兜底，保证不返回 null

            return instance;
        }
    }
}
```

- 泛型约束 `where T : ScriptableObject`；
- 约定路径 `Resources/ScriptableObject/类名` 懒加载；
- 加载失败时 `CreateInstance` 兜底，避免空引用；
- 子类只需 `class GlobalConfig : SingScriptableObject<GlobalConfig> { }` 就获得单例访问。

## 三个必须知道的坑

**1. 运行期改 SO 会污染资产**

在 Play 模式下修改 SO 字段，退出后**修改会保留**（编辑器不会回滚资产）。调试时改数值一定要用副本。

**2. 打包后 SO 是只读的**

把玩家进度写进 SO，编辑器里看起来正常，打包后写入无效。**SO 只放设计期数据，运行期数据走存档系统。**

**3. SO 之间循环引用会导致序列化失败**

`A` 引用 `B`、`B` 又引用 `A` 时，Unity 序列化会出问题。设计上保持单向引用。

## 目录结构

```
Assets/Scripts/
├── Lesson1数据文件创建/            MusicData、MyData、ScriptableObjectTool
├── Lesson2_数据文件的使用/          SettingPanel
├── Lesson3_非持久数据/             SettingPanel2
├── Lesson4_让非持久数据持久化/       SettingInfo、SettingPanel3
├── Lesson5_ScriptableObject的应用 - 配置数据/   RoleInfo
├── Lesson6_ScriptableObject的应用 - 数据复用/   BulletInfo、bullet
├── Lesson7_ScriptableObject的应用 - 数据带来的多态行为/  AudioBase、RandoPlayAudio
└── Lesson8_ScriptableObject的应用- 单例模式的获取数据/  SingScriptableObject
```

## 适用边界

- **适合**：怪物属性表、技能配置、掉落表、音频/特效资源集合、需要多态行为的数据；
- **不适合**：玩家存档（打包后只读）、需要热更的数值（SO 随包发布，改了要重新出包）、大量离散数值（用 Excel 导表更合适）。
