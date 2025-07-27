# GitHub Copilot Instructions for RimWorld Mod Project

## Mod Overview and Purpose

This RimWorld mod is designed to enhance the storytelling and event systems within the game by adding a variety of new events and interactions between the characters (pawns). It focuses on improving in-game social dynamics and memorializing significant events in the colony's history.

## Key Features and Systems

- **Event System**: Introduces a suite of new events (e.g., raids, marriages, funerals, recruitment attempts) handled through the `IEvent` interface, allowing for dynamic interactions.
- **Funeral Mechanics**: Adds depth to the funeral process with specific job drivers and think nodes that allow pawns to attend and participate in funerals.
- **Social Interactions**: Enhances the social aspect of the game, including bond developments, breakups, and relationship changes.
- **Notification Framework**: Provides notifications for important events, ensuring players are fully informed about significant story developments.

## Coding Patterns and Conventions

- **Internal Classes**: The mod primarily uses `internal` class access modifiers to keep implementation details encapsulated within the mod's assembly, promoting clear interfaces for interaction.
- **Consistent Naming**: Use of descriptive class and method names that clearly reflect their purpose and functionality within the mod.
- **Interface Implementation**: Many classes implement the `IEvent` interface, ensuring a consistent pattern for event management with mandatory methods for event lifecycle handling.
- **Static Utilities**: Utilizes static utility classes and methods to share common functionalities across the mod modules.

## XML Integration

While the XML files for this mod are not detailed here, it is worth noting how XML typically integrates with RimWorld mods:
- **Defining Objects and Recipes**: XML files in RimWorld often define game objects, recipes, and settings that can be referenced and manipulated within the C# code.
- **PatchOperations**: XML can be used to patch existing RimWorld definitions, which is crucial for modifying core game behavior non-destructively.

## Harmony Patching

The Harmony library is a pivotal tool used in this mod for runtime method patching, allowing for safe modification of core game behaviors without altering the original game code:
- **Prefix and Postfix Methods**: Utilizing Harmony's prefix and postfix patching to inject additional logic before or after the execution of existing methods.
- **Targeted Modifications**: Only applying patches to specific game classes and methods to maintain compatibility and performance.
  
## Suggestions for Copilot

- **Code Completion**: Generate boilerplate code for implementing new `IEvent` types, ensuring consistency with existing event classes.
- **Harmony Patterns**: Provide code snippets for common Harmony patching patterns, particularly for methods in core classes like `Pawn` or `Map`.
- **Method Stubs**: Assist in creating method stubs for lifecycle methods (`TryStartEvent`, `EndEvent`, etc.) when implementing new event types.
- **Utility Functions**: Suggest utility functions for common tasks such as date calculations, pawn selection criteria, and thought additions.
- **XML Schema Generation**: Recommend XML schema templates for new in-game objects or recipes whenever new features require XML configuration.
