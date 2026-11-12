# GitHub Copilot Instructions for RimStory (Continued)

## Mod Overview and Purpose

**RimStory (Continued)** is a mod for **RimWorld** that enhances the storytelling experience by logging significant events in the colony's history. It aims to bring back the nostalgic moments from the early days of RimWorld, such as funerals, marriage anniversaries, memorial days, and days of great victory.

## Key Features and Systems

- **Funerals:** Logs and commemorates the deaths of colonists.
- **Marriage Anniversaries:** Celebrates significant milestones in relationships.
- **Memorial Day:** Honors fallen comrades.
- **Days of Great Victory:** Marks the colony's triumphs.
- **Individual Thoughts:** Records and displays personal thoughts and memories.

## Coding Patterns and Conventions

- **C# Naming Conventions:**
  - Use PascalCase for class names and method names.
  - Use camelCase for local variables and parameters.
  - Prefix interfaces with an `I`.
  - Prefix abstract classes with an `A`.

- **XML Naming Conventions:**
  - Use PascalCase for XML tags and attributes.
  - Ensure all XML files are properly indented and formatted.

- **Error Handling:**
  - Use try-catch blocks for critical operations to prevent crashes.
  - Log errors and provide meaningful error messages.

- **Code Comments:**
  - Write clear and concise comments for complex logic.
  - Use XML documentation comments for public methods and classes.

## XML Integration

- **Def Files:**
  - RimStory uses XML files to define various game elements such as main buttons, think tree duties, thought definitions, and update information.
  - These files are located in the `Defs` folder within the mod's directory.
  - Ensure that all XML files are correctly referenced in the `ModMetaData.xml` file.

- **Def Types:**
  - **MainButtonDef:** Defines custom buttons in the main UI.
  - **DutyDef:** Defines custom duties for pawns.
  - **ThoughtDef:** Defines custom thoughts and memories.
  - **TabulaRasa.UpdateDef:** Provides update information.

## Harmony Patching

- **Harmony Patches:**
  - RimStory uses Harmony to patch existing game methods to add custom functionality.
  - Patches are located in the `Harmony` folder within the mod's directory.
  - Ensure that all Harmony patches are correctly applied and do not interfere with other mods.

- **Patch Naming Conventions:**
  - Use PascalCase for patch class names.
  - Use camelCase for patch method names.
  - Prefix patch classes with the name of the patched method.

- **Patch Structure:**
  - Implement `Postfix` or `Prefix` methods to modify the behavior of existing methods.
  - Use `Transpiler` methods for more advanced patching.

## Suggestions for Copilot

- **Custom Events:**
  - Encourage the creation of new custom events to enhance the storytelling experience.
  - Provide examples of existing custom events for reference.

- **Localization:**
  - Assist in the translation of RimStory to new languages.
  - Ensure that all text in XML files is properly localized.

- **Performance Optimization:**
  - Suggest optimizations for performance-critical sections of the code.
  - Ensure that all patches and custom logic are optimized to minimize performance impact.

- **Bug Fixes:**
  - Assist in identifying and fixing bugs reported by the community.
  - Provide detailed logs and error messages for debugging.

- **Documentation:**
  - Help in writing clear and concise documentation for new features and changes.
  - Ensure that all public methods and classes are well-documented.

## Contributing to RimStory (Continued)

- **Reporting Issues:**
  - Use the Discord channel for error reporting.
  - Always provide detailed logs using the Log Uploader.
  - Avoid reporting errors via discussion threads; use the GitHub repository instead.

- **Proposing Changes:**
  - Propose new features or improvements through the GitHub repository.
  - Follow the coding patterns and conventions outlined in this document.

- **Testing:**
  - Test new features and patches thoroughly before merging into the main branch.
  - Ensure compatibility with other mods and versions of RimWorld.

## Thanks

- **Serpyderpy** for the logo.
- **Mehni** for modding tips.
- **Crioware** for translations.
- **SoggyNoodle** for all contributions.
- **Lonely_Goat** for testing.

## Translations

- **English**
- **Polish**
- **Japanese** by Proxyer
- **Russian** by GidReess
- **German** by Lauri7x3
- **Chinese** by gretino
- **Korean** by soocheol747

## Dependencies

- **brrainz.harmony**
- **UnlimitedHugs.HugsLib**

## Credits

- **Khaligufzel** for the original mod.
- **m1st4x** for updates and maintenance.
- **Community** for translations and feedback.

---

By following these guidelines, you can contribute to and enhance the **RimStory (Continued)** mod, making it even more engaging and enjoyable for players of **RimWorld**.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

