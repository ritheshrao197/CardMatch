# CardMatch

A memory card matching game built with Unity.

## Overview

CardMatch is a classic memory game where players flip cards to find matching pairs. The game features multiple levels with increasing difficulty, beautiful graphics, and smooth gameplay.

## Features

- **Progressive Difficulty**: 30 levels with increasing grid sizes and challenges
- **Multiple Game Modes**: Time-limited and move-limited challenges
- **Beautiful Graphics**: Attractive card designs and UI elements
- **Audio System**: Sound effects and background music with volume controls
- **Progress Tracking**: Save and track your progress through levels
- **Responsive Design**: Works on both mobile and desktop devices

## Game Mechanics

### Basic Gameplay
1. Flip two cards at a time to find matching pairs
2. If the cards match, they remain face up
3. If they don't match, they flip back over
4. Complete the level by finding all pairs

### Level Challenges
- **Easy Levels (1-10)**: Small grids with no restrictions
- **Medium Levels (11-20)**: Larger grids with move limits
- **Hard Levels (21-30)**: Large grids with time limits and move restrictions

## Project Structure

```
Assets/
├── Scripts/
│   ├── Constants/      # Game constants and configuration
│   ├── Controllers/    # Game logic controllers
│   ├── Data/           # Data models and configurations
│   ├── Events/         # Event system
│   ├── Services/       # Game services
│   ├── UI/             # User interface components
│   └── Utilities/      # Utility classes
├── Resources/          # Game assets
└── Scenes/             # Unity scenes
```

## Technical Architecture

### Core Components

1. **BoardController**: Manages the game board layout and card placement
2. **CardController**: Handles individual card behavior and interactions
3. **MatchService**: Validates card matches and manages game state
4. **LevelRules**: Enforces level-specific rules and constraints
5. **UIFlow**: Manages the overall UI navigation and flow

### Key Systems

- **Object Pooling**: Efficient card object management
- **Event System**: Loose coupling between components
- **Progress System**: Player progress tracking and level unlocking
- **Audio System**: Sound effects and music management

## Getting Started

### Prerequisites
- Unity 2021.3 LTS or later
- Basic understanding of Unity and C#

### Setup
1. Clone the repository
2. Open the project in Unity
3. Open the main scene
4. Press Play to start the game

### Controls
- **Mouse/Touch**: Click or tap cards to flip them
- **UI Buttons**: Use the HUD buttons to pause, restart, or navigate

## Development

### Code Organization
The project follows a modular architecture with clear separation of concerns:
- **Constants**: Centralized configuration values
- **Controllers**: Game logic and behavior
- **Data**: Models and configurations
- **Services**: Core functionality
- **UI**: Interface components
- **Utilities**: Reusable helper classes

### Adding New Levels
1. Modify `LevelConfig.cs` to add new level definitions
2. Adjust difficulty parameters (grid size, move limits, time limits)
3. Test the new levels

### Customizing Graphics
1. Replace card sprites in the Resources folder
2. Update UI elements in the Canvas prefabs
3. Adjust colors and themes through the UI system

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Graphics and UI assets from Belevich Hyper casual mobile GUI
- Audio assets from FREE CASUAL MUSIC PACK
- Font assets from various free sources
