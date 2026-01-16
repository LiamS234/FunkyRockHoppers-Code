# Funky Rock Hoppers — Code Samples

**Project:** Funky Rock Hoppers (Steam, free)  
**Role:** Solo developer — code, 3D art, audio, UI  
**Unity:** 2022.3.13f1  
**What’s here:** A small selection of key C# scripts demonstrating core gameplay, infinite procedural platform generation, and environment placement/cleanup.

> Note: these three scripts are from an earlier stage of my development work. I’ve intentionally left them as-is to show my learning curve; more refined systems appear in later projects included in my portfolio (for example, the Random Prison Map Generator).

## Play the game
- Play in browser (itch.io): https://liamsmith234.itch.io/funky-rock-hoppers  
- Steam page: https://store.steampowered.com/app/2972330/HFH/  
- Trailer: On Steam Page

## Included files (recommended for reviewers)
These files capture the systems most important to the game’s core loop and procedural tech.

- `GenerateInfinite.cs`  
  **Why include:** Central procedural generation controller — demonstrates player-aware generation (only create tiles when the player reaches a threshold), physics-constrained gaps, and tile lifecycle management.  
  **Overview:** Decides when to spawn new tiles, determines tile sizes and gap distances using player jump physics and mesh-vertex data, instantiates tiles, schedules camera-mode ranges, and destroys old tiles. This file reveals how geometry, player capabilities and camera state are tied into generation.

- `PlayerController.cs`  
  **Why include:** Shows polished player movement and jump feel — multiple raycasts, velocity smoothing, and camera-mode aware controls.  
  **Overview:** Rigidbody-driven controller that samples input in `Update()` and applies physics in `FixedUpdate()`. Implements distance-based speed scaling, AnimationCurve-driven interpolation for smooth speed changes, multi-ray grounding / slope alignment, short/long jump behavior (gravity multipliers), limited mid-air flaps, and collision/game-over checks.

- `StationaryScenerySpawner.cs`  
  **Why include:** Environment system that takes spawn decisions from a placement-check subsystem and instantiates/cleans up props safely.  
  **Overview:** Listens for tile-update events, receives validated spawn lists from `EmptyScenSpawner`, instantiates scaled prefabs, orients certain props toward the player, and removes scenery when behind the player. Includes special handling for long platforms and collects height references used by aerial systems.

## How to read this repo
- Start with `GenerateInfinite.cs` — it is the high-level orchestration for level progression and will help you understand when/why tiles are spawned.  
- Read `PlayerController.cs` next to see how generation constrains (jump velocity, speed) are derived from player physics and how input is handled.  
- Finally open `StationaryScenerySpawner.cs` to see how visuals are instantiated safely and cleaned up.  
- Each file includes inline comments explaining non-obvious decisions (mesh vertex indexing, camera switch windows, and lifetime bounds).

## Notes on these code samples
- These excerpts are intentionally focused on systems and algorithms.  
- The code contains practical Unity patterns (separating `Update` input sampling from `FixedUpdate` physics, event-style flags for single-run work, deterministic cleanup) as well as some legacy/global state usage from an earlier development stage. If you’re curious, my later project in this portfolio demonstrates a more refactored approach.

## Contact
Liam Smith — liam.smith234@gmail.com — https://liamsmith234.itch.io/
