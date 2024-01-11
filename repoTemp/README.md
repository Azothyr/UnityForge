# Unity C# Script Toolbox
## Introduction

In this repository, you will find various scripts used for or as a base to create games/content in the game engine Unity.

## Explanation

- **Actions**: Contains scripts related to action handling. Sub-folders for different types of handlers and interfaces.
- **Audio**: Scripts related to audio management.
- **BuiltIns**: Fundamental scripts used across multiple modules. Sub-folder for different data types.
- **CustomEditors**: Custom editor and property drawer scripts for Unity Editor.
- **DataContainers**: Scripts for managing and storing data.
- **Debug**: Scripts for debugging and related functionalities.
- **Handlers**: Scripts that handle game logic and interactions. Categorized by functionality.
- **InputSystem**: Scripts for managing game input.
- **Interfaces**: Interface definitions.
- **Multiplayer**: Scripts for managing multiplayer aspects.
- **Placement**: Scripts related to object placement and building mechanisms in-game.
- **ScriptableObjects**: Scripts defining various scriptable objects.
- **UI**: User interface related scripts.
- **Upgrades**: Scripts managing upgrades and related logic.

### Hierarchy Visual:

<details>
    <summary>UnityForge/</summary>
<pre><code>│
│
├── Actions/
│   ├── Handlers/
│   │   ├── AdvancedActionHandler.cs
│   │   ├── BasicActionHandler.cs
│   │   └── GameActionHandler.cs
│   │
│   ├── Interfaces/
│   │   ├── IDBehavior.cs
│   │   ├── MatchIDBehavior.cs
│   │   └── SimpleMatchIDBehavior.cs
│   │
│   ├── GameAction.cs
│   └── ActionCoroutineSO.cs
│
├── Audio/
│   └── AudioBehavior.cs
│
├── BuiltIns/
│   ├── DataTypes/
│   │   ├── BoolData.cs
│   │   ├── DoubleData.cs
│   │   ├── FloatData.cs
│   │   ├── IntData.cs
│   │   ├── Vector2Data.cs
│   │   ├── Vector3Data.cs
│   │   └── Vector3DataList.cs
│   │
│   ├── CameraUtility.cs
│   ├── CharacterData.cs
│   ├── ClickData.cs
│   ├── CoroutineBehavior.cs
│   ├── CurrencyBehavior.cs
│   └── GameObjData.cs
│
├── CustomEditors/
│   ├── Editors/
│   │   ├── ButtonEditor.cs
│   │   └── CharacterDataEditor.cs
│   │
│   └── PropertyDrawers/
│       ├── StepDrawer.cs
│       └── StepAttribute.cs
│
├── DataContainers/
│   ├── TileArrayData.cs
│   ├── TileData.cs
│   └── TransformArrayData.cs
│
├── Debug/
│   ├── Debugger.cs
│   ├── DebugManager.cs
│   ├── DestroyBehavior.cs
│   ├── EnemyData.cs
│   ├── GameManager.cs
│   └── GameObjectList.cs
│
├── Handlers/
│   ├── Controllers/
│   │   ├── CreepController.cs
│   │   ├── EnemyController.cs
│   │   ├── GameTimeController.cs
│   │   ├── PlayerController.cs
│   │   ├── RBControllerBase.cs
│   │   └── TowerController.cs
│   │
│   ├── Environment/
│   │   ├── Generate3DNavMeshSurface.cs
│   │   ├── GridManager.cs
│   │   └── ObjGenOnGrid.cs
│   │
│   ├── Instancing/
│   │   ├── Instancer.cs
│   │   ├── SpawnBehavior.cs
│   │   └── SpawnManager.cs
│   │
│   └── Time/
│       ├── TimerBehavior.cs
│       └── WaitBehavior.cs
│
├── InputSystem/
│   ├── Controls.cs
│   ├── InputActions.cs
│   ├── InputReader.cs
│   └── UIInterface.cs
│
├── Interfaces/
│   ├── ICollidableRB.cs
│   ├── IDamagable.cs
│   ├── IDamageDealer.cs
│   ├── IDrawGizmo.cs
│   ├── INeedButton.cs
│   └── IUpdateOnChange.cs
│
├── Multiplayer/
│   ├── ClientNetworkTransform.cs
│   └── ConnectionManager.cs
│
├── Placement/
│   ├── BuildingManager.cs
│   ├── TowerBuildManager.cs
│   ├── PlayerData.cs
│   ├── PrefabData.cs
│   └── PrefabDataList.cs
│
├── ScriptableObjects/
│   ├── CreepData.cs
│   ├── CreepPrefabData.cs
│   ├── PlayerAccomplishmentData.cs
│   ├── SpawnerData.cs
│   └── TileDataList.cs
│
├── UI/
│   └── TextMeshProBehavior.cs
│
└── Upgrades/
    ├── FloatUpgrade.cs
    ├── IntUpgrade.cs
    ├── UpgradeBase.cs
    └── UpgradeManager.cs</code></pre>
</details>

## Contact Information

- **Email**: Peterson.Zac17@gmail.com
---

Thank you for visiting my repository, and I hope you find my work interesting and informative!

Zac Peterson - © 2024