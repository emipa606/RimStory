# Copilot Instructions for RimStory (Continued)

## Mod Overview and Purpose
RimStory is a mod for RimWorld designed to enrich the storytelling aspect of the game. It logs important events in a colony's life, such as deaths, marriages, epic battles, and failures. By doing so, it helps players remember the significant moments in their colonies and encourages deeper engagement with their pawns and their stories.

## Key Features and Systems
- **Funerals**: Host memorial services for fallen colonists.
- **Marriage Anniversaries**: Celebrate the unions within your colony.
- **Memorial Day**: Honor the memory of those lost on specific days.
- **Days of Great Victory**: Commemorate significant battles and triumphs.
- **Individual Thoughts**: Pawns reflect on personal and communal events.
- **Anniversary Events**: Pawns organize celebrations to remember key dates.
- **Compatibility**: RimStory is highly compatible with other mods but advises configuration settings to avoid redundant events when used with mods like Psychology and Days Matter.

## Coding Patterns and Conventions
- **Class Structure**: All classes are internal by default unless they need wider access.
- **Method Naming**: Follows C# naming conventions, with method names beginning in uppercase (e.g., `TryStartEvent`, `ExposeData`).
- **Interface Implementation**: Uses interfaces like `IEvent` to define methods shared by multiple classes.
- **Stateless Helpers**: Utilizes static classes for utility functions (`Utils`, `Resources`, etc.), promoting stateless designs.

## XML Integration
- **Translation Support**: RimStory supports multiple languages through XML files. Contributors can submit translation files for new languages to be added.
- **Event Definitions**: Event details and configurations may be defined in XML files, allowing for easy adjustments and additions without altering the codebase.

## Harmony Patching
- RimStory uses Harmony to safely modify the base game's functionalities without directly altering original game code.
- Patches are targeted at specific RimWorld methods, ensuring compatibility and reducing code conflicts with other mods.

## Suggestions for Copilot
1. **New Event Ideas**: Suggest adding new events that could be logged by the system, such as natural disasters, or first alien contact.
2. **Performance Optimization**: Recommend code improvements that streamline data logging and event processing.
3. **Code Refactoring**: Identify opportunities for abstracting common functionalities into base classes or interfaces to reduce code duplication.
4. **Translation Integration**: Assist with adding translation capability by suggesting consistent XML structure and key naming conventions.
5. **Harmony Patch Suggestions**: Provide insights on effective use of Harmony for patching game mechanics, recommending patches only when necessary.

By following this guide and utilizing GitHub Copilot, contributors can effectively develop and expand the RimStory mod while maintaining code quality and compatibility.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
