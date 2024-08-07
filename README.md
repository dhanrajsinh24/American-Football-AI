# Football Pursuit Simulation in Unity 2022.3.27f1

## Overview
This project simulates a football player attempting to score a touchdown while being pursued by defenders. The ball carrier is controlled by the user, and the defenders are controlled by NPCs with varying speeds.

## Graphical Assets
- Used placeholder assets for everything including field, players.

## Controls
- Use the arrow keys or WASD to move the ball carrier.

## Implementation (in scene ExerciseScene.unity)
Player
- IMovable.cs: Interface to define movement behavior.
- PlayerInitialization.cs: Manages the player initialization and positions of ball carrier and NPC defenders.
- PlayerMovements.cs: Manages the ball carrier and defender movements with acceleration and deceleration.

Ball Carrier
- BallCarrier.cs: Represents the ball carrier with properties for movement and interactions.
- BallCarrierMovements.cs: Handles the ball carrier's movement logic.

Defender NPCs
- Defender.cs: Represents a defender NPC and initializes it with a strategy and state machine.
- DefenderMovements.cs: Manages the movement logic for defenders.
- DefenderFactory.cs: Factory class to create NPC defenders.
- IDefenderState.cs: Interface for defender states.
- PursuingState.cs: State where the defender pursues the ball carrier.
- TackledState.cs: State where the defender tackles the ball carrier.
- DefenderStateMachine.cs: Manages the states of the defender.
- IDefenderStrategy.cs: Interface for defender strategies.
- AngledPursuit.cs: Strategy for calculating pursuit angles based on the target's position and speed.
- FormationDefense.cs: Strategy for NPC defender movement that maintains a defensive formation to stop the ball carrier.

Utility
- PlayerInfo.cs: Contains information about players such as position and speed.
- CameraFollow.cs: Implements a third-person camera view that follows the ball carrier.

## Setup
1. Open the Unity project.
2. Play the scene to control the ball carrier and observe the defenders' pursuit behavior.
