namespace Gum.ProjectServices.CodeGeneration;

/// <summary>
/// Detects the syntax version of the Gum runtime referenced by a game project.
/// </summary>
public interface ISyntaxVersionDetectionService
{
    /// <summary>
    /// Resolves the effective syntax version for code generation, taking into account
    /// the user's <see cref="CodeOutputProjectSettings.SyntaxVersion"/> setting.
    /// Returns <c>"*"</c> for auto-detect or an explicit version number.
    /// </summary>
    SyntaxVersionResult Detect(CodeOutputProjectSettings settings, string? projectDirectory);

    /// <summary>
    /// Detects the C# language version (major number) the game project compiles with, from its
    /// .csproj's <c>LangVersion</c> or, when absent, its target framework's default. Returns null
    /// when it can't be determined or is an open-ended value like <c>latest</c>, meaning codegen
    /// can use the newest syntax it emits.
    /// </summary>
    int? DetectCSharpLanguageVersion(CodeOutputProjectSettings settings, string? projectDirectory);

    /// <summary>
    /// Whether the game project is a Unity project, detected from its .csproj referencing
    /// <c>UnityEngine</c>. Unity doesn't run module initializers, so codegen registers types
    /// through Unity's own startup hook instead.
    /// </summary>
    bool DetectIsUnityProject(CodeOutputProjectSettings settings, string? projectDirectory);
}
