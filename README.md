# <u>UnityForge:</u>
*Unity C# Script Toolbox*

---

## Introduction

In this repository, you will find various scripts used for or as a base to create games/content in the game engine Unity.

---

## Quick Description

- #### Actions: 
    Scripts for action handling and response, including handlers for different action types and trigger comparison logic.
- #### Audio:
    Centralized scripts for managing audio playback and audio-related functionalities.
- #### CoreFacilitators:
    Core game logic and control scripts, including controllers for different game entities, environmental interactions, instantiation logic, removal behaviors, and time-related functionalities.
- #### CustomEditor:
    Scripts enhancing the Unity Editor experience, including custom editors and property drawers.
- #### DataHandlers:
    A collection of scripts managing and organizing data. This includes entity archetypes like character, enemy, and object data, as well as basic data types like integers, floats, and vectors.
- #### Debug:
    Tools and utilities for debugging purposes, providing insights during development and testing.
- #### InputSystem:
    Scripts dedicated to handling and processing player input, integrating with Unity's input system.
- #### Interface:
    Definitions of interfaces used across various scripts to ensure conformity and interoperability between components.
- #### Multiplayer:
    Essential scripts for handling multiplayer aspects like network transform synchronization and connection management.
- #### Placement:
    Scripts that manage the in-game placement of objects and structures, including building and tower construction.
- #### ScriptableObject:
    Definitions of various ScriptableObjects, facilitating data storage and management without relying on MonoBehaviours.
- #### UI:
    Scripts related to the user interface, including those that manage UI elements and interactions.
- #### Upgrade:
    Management of in-game upgrades and enhancements, including scripts for handling upgrade logic and currency interactions.

---

<details><summary style="font-size: x-large; font-weight: bold">Hierarchy Visual:</summary>
<pre><code><b>UnityForge</b>
├── Actions/
│   ├── Handlers/
│   │   ├── ActionHandler.cs
│   │   └── ActionHandlerBase.cs
│   │
│   ├── TriggerCompare/
│   │   ├── IDBehavior.cs
│   │   ├── MatchIDBehavior.cs
│   │   └── MatchIDBehaviorBase.cs
│   │   
│   ├── ActionCoroutineSO.cs
│   └── GameAction.cs
│
├── Audio/
│   └── AudioBehavior.cs
│
├── CoreFacilitators/
│   ├── Controllers/
│   │   ├── CreepController.cs
│   │   ├── FollowBehavior.cs
│   │   ├── NavAgentBehavior.cs
│   │   ├── NavAgentBehaviorBase.cs
│   │   ├── PlayerAgentBehavior.cs
│   │   ├── RBControllerBase.cs
│   │   ├── RBEnemyController.cs
│   │   ├── RBPlayerController.cs
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
│   ├── Removal/
│   │   └── DestroyBehavior.cs
│   │
│   ├── Time/
│   │   ├── GameTimeOperator.cs
│   │   ├── TimerBehavior.cs
│   │   └── WaitBehavior.cs
│   │
│   └── GameManager.cs
│
├── CustomEditor/
│   ├── Editors/
│   │   ├── ButtonEditor.cs
│   │   └── CharacterDataEditor.cs
│   │
│   └── PropertyDrawers/
│       ├── StepAttribute.cs
│       └── StepDrawer.cs
│
├── DataHandlers/
│   ├── EntityArchetypes/
│   │   ├── CharacterData.cs
│   │   ├── CreepData.cs
│   │   ├── CreepPrefabData.cs
│   │   ├── EnemyData.cs
│   │   ├── GameObjData.cs
│   │   ├── GameObjectList.cs
│   │   ├── PlayerData.cs
│   │   ├── PrefabData.cs
│   │   └── PrefabDataList.cs
│   │
│   ├── Primitives/
│   │   ├── BoolData.cs
│   │   ├── DoubleData.cs
│   │   ├── FloatData.cs
│   │   ├── IntData.cs
│   │   ├── Vector2Data.cs
│   │   ├── Vector3Data.cs
│   │   └── Vector3DataList.cs
│   │
│   ├── CameraUtility.cs
│   ├── ClickData.cs
│   ├── CoroutineBehavior.cs
│   ├── ID.cs
│   ├── MeshBehavior.cs
│   ├── SceneBehavior.cs
│   ├── ScoreBehavior.cs
│   ├── TileArrayData.cs
│   ├── TileData.cs
│   ├── TileDataList.cs
│   └── TransformArrayData.cs
│
├── Debug/
│   ├── Debugger.cs
│   └── DebugManager.cs
│
├── Input System/
│   ├── Controls.cs
│   ├── GameInputsSO.cs
│   ├── Input Actions.cs
│   ├── InputReader.cs
│   └── InteractionHandler.cs
│
├── Interface/
│   ├── ICollidableRB.cs
│   ├── IDamagable.cs
│   ├── IDamageDealer.cs
│   ├── IDrawGizmo.cs
│   ├── INeedButton.cs
│   └── IUpdateOnChange.cs
│
├── Multiplayer/
│   ├── ClientNetworkTransform.cs
│   ├── ConnectionManager.cs
│   └── PlayerMovement.cs
│
├── Placement/
│   ├── BuildingManager.cs
│   └── TowerBuildManager.cs
│
├── ScriptableObject/
│   ├── PlayerAccomplishmentData.cs
│   └── SpawnerData.cs
│
├── UI/
│   └── TextMeshProBehavior.cs
│
└── Upgrade/
    ├── CurrencyBehavior.cs
    ├── FloatUpgrade.cs
    ├── IntUpgrade.cs
    ├── UpgradeBase.cs
    └── UpgradeManager.cs
</code></pre>
</details>

---

## Contact Information

- **Email**: Peterson.Zac17@gmail.com

---

Thank you for visiting my repository, and I hope you find my work interesting and informative!

Zac Peterson - © 2024

---