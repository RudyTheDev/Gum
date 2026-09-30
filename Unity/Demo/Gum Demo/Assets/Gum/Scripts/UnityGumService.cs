using Gum;

/// <summary>
/// The concrete Skia GumService for the Unity host. SkiaGum ships only the abstract
/// <see cref="GumServiceSkiaBase"/>; the default concrete Gum.GumService lives in SkiaGum.Standalone,
/// which isn't published to Unity. Input (CreateCursor/CreateKeyboard) is not wired up yet.
/// </summary>
public class UnityGumService : GumServiceSkiaBase
{
    static UnityGumService _default;

    public static UnityGumService Default => _default ??= new UnityGumService();
}
