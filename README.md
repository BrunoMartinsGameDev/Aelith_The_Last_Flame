# Aelith: The Last Flame

2D Metroidvania developed solo in Unity 6 as a portfolio project.

## Tech Stack
- Unity 6 URP
- C# with Scriptable Object-driven architecture
- Cinemachine 3
- Unity Animator (code-controlled, zero transitions)

## Architecture Highlights
- ScriptableObjects for all game data (PlayerStats, EnemyData, 
  AttackData, LootTable, Dialogue, Abilities)
- Combo system with per-step configurable damage and hitbox via SO
- Ability unlock system — dash and shield gate area progression
- Observer-ready component separation (PlayerController, PlayerJump,
  PlayerCombat, PlayerAnimator, PlayerHealth)

## Status
🚧 In development — Week 1 of 2