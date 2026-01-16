# Architecture Overview

**Project:** Funky Rock Hoppers

## High-level design
- Component-driven, single-scene structure. Systems are split into:
  - Player (movement & input)
  - Level generation (tile creation & spacing)
  - Environment (scenery placement & cleanup)
  - Camera logic (handled via ranges created by the generator)

- Design goal: deterministic, player-aware procedural generation that guarantees jumpable gaps and coherent camera transitions.

## Core subsystems & responsibilities

1. **GenerateInfinite.cs (procedural controller)**  
   - Entry point for tile creation.  
   - Only spawns new tiles when the player has progressed a threshold distance (player-driven spawning).  
   - Uses player parameters (jump velocity, gravity multipliers, speed) and mesh vertex data to compute safe gap sizes and tile heights.  
   - Tracks camera mode ranges (side-on / switch windows) and long-platform sections.  
   - Instantiates/destroys tile GameObjects and sets flags consumed by other spawners (birds, fish, scenery).

2. **PlayerController.cs (movement & physics)**  
   - Rigidbody-based movement, sampling input in `Update()` and applying movement/jump logic in `FixedUpdate()`.  
   - Implements smooth speed interpolation via an `AnimationCurve`, short/long jump feel with gravity multipliers, and multiple raycasts for robust grounding and slope alignment.  
   - Exposes metrics used by the generator (e.g., `metresTravelled`, `jumpVelocity`) so generation is constrained by player capability.

3. **StationaryScenerySpawner.cs (env. instantiation & cleanup)**  
   - Consumes validated spawn lists produced by a placement checker (`EmptyScenSpawner`) and instantiates scaled prefabs at the correct Y offset.  
   - Rotates/animates certain props (e.g., snowmen face the player).  
   - Maintains a delete boundary and removes objects when behind the player to limit memory use.  
   - Special-cases long platforms for placement bounds (entrance spacing, camera safety).

## Data flow (recommended read order)
1. `GenerateInfinite` calculates when new tiles are required and sets flags / spawner inputs.  
2. The generator and auxiliary spawners produce validated spawn data for scenery/enemies.  
3. `StationaryScenerySpawner` consumes validated lists and instantiates prefabs.  
4. `PlayerController` provides runtime metrics used by the generator and consumes camera-switch ranges to change control logic.

## Notable design & implementation choices
- **Player-aware generation:** gaps and heights are computed using projectile motion formulas so the world remains fair and playable.  
- **Mesh-level alignment:** mesh vertex values are read to align subsequent tile geometry and ensure smooth seams between tiles.  
- **Event-style delegation:** generator sets flags (`scenTileUpdate`, etc.) so subsystems run one-time processing per tile update instead of every frame.  
- **Performance conscious:** explicit cleanup windows for scenery, avoiding per-frame allocations where possible; smaller prototypes avoid object pooling for static props to keep iteration fast.

## Testing & performance notes
- Designed for a 60 FPS target on desktop; WebGL builds used simplified colliders and baked lighting to reduce runtime cost.  
- The generator is deterministic per run timestamp/seed; many variables are exposed as tunables (tile scale bounds, curve for speed feel) so designers can iterate without code changes.

## Tips for reviewers
- Read `GenerateInfinite.cs` first to understand the generation trigger and gap/height calculations. Look for the physics formulae that constrain gap sizing.  
- Open `PlayerController.cs` to verify grounding, slope alignment, and curve-driven speed transitions. That demonstrates game-feel polish.  
- Inspect `StationaryScenerySpawner.cs` to see how validated spawn data is consumed and how cleanup is handled deterministically.

## Notes on code provenance
- These scripts are from an earlier stage of the project and intentionally left in their original form to show evolution in coding style; later portfolio projects include more refactored implementations.

## Contact
Liam Smith — liam.smith234@gmail.com — https://liamsmith234.itch.io/
