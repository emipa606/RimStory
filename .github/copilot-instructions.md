# GitHub Copilot Instructions for RimStory (Continued)

## Mod Overview and Purpose

**RimStory (Continued)** is a storytelling mod for RimWorld aimed at enhancing the game's narrative aspect by logging important colony events. The mod records pivotal moments like deaths, marriages, epic battles, and failures to provide a rich historical context for your colony. Colonists will celebrate anniversaries, remember fallen comrades, and more, enriching the player's experience through stories and memories.

## Key Features and Systems

- **Funerals**: Arrange funerals for deceased colonists, enhancing emotional connections within the colony.
- **Marriages and Anniversaries**: Celebrate weddings and anniversaries, contributing to the colonists' happiness and interconnections.
- **Memorial Day**: Commemorate critical events or fallen heroes who have left a mark on the colony.
- **Great Victories**: Log and celebrate your colony's most significant triumphs.
- **Individual Thoughts**: Colonists will form unique thoughts and perspectives based on events they experience.

## Coding Patterns and Conventions

- **Class Structure**: The mod employs internal classes extensively, indicating a focus on modular and encapsulated design. Classes like `IncidentWorker`, `LordToil`, and `JobDriver` suggest the use of object-oriented principles to handle event-driven behaviors systematically.
- **Namespace Use**: Ensure consistent and clear use of namespaces to organize classes logically.
- **Interfaces and Inheritance**: Utilization of interfaces such as `IEvent` and inheritance from existing RimWorld classes (`JobDriver`, `LordToil`) indicate a pattern of leveraging existing game structures for seamless integration.
- **Comments and Documentation**: Ensure methods and classes are well-documented with comments to facilitate understanding and future development.

## XML Integration

- **Translations**: The mod supports multiple languages via XML files. Any new events or text should be integrated through XML to ensure localization support.
- **Defs Handling**: Utilize XML for defining new events, thoughts, and incidents to maintain compatibility and ease updatability across game versions.
  
## Harmony Patching

- **Harmony Patching**: Ensure Harmony is properly set up and maintained within your project. This library is used to patch existing game code, allowing RimStory to append or modify behavior without altering original code.
- **Patch Targets**: Focus on patching relevant RimWorld classes that handle event management, such as `Pawn_Kill` or `IncidentWorker`.

## Suggestions for Copilot

- Utilize Copilot for boilerplate code and repetitive tasks such as event logging and consistency checks.
- Leverage Copilot for generating test methods or scenarios to ensure new features work as intended without breaking existing functionalities.
- Use Copilot to draft XML files for new translations or event definitions.
- Suggest code optimization opportunities, particularly for performance-sensitive areas where reflection and dynamic code execution are involved.
- When patching new game mechanic code, utilize Copilot to automate the creation of reliable Harmony patches that minimize the risk of bugs and maintain compatibility.

By adhering to these instructions and leveraging GitHub Copilot effectively, you can enhance the RimStory mod while ensuring maintainability and compatibility across future RimWorld updates. Collaborative development and user feedback will continue to shape and polish the storytelling experience within RimWorld's rich, dynamic world.


This `.github/copilot-instructions.md` file should help guide contributors using GitHub Copilot, focusing on the specifics of mod development in RimWorld using C#.
