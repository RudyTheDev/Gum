// Unity's GumService: a GumServiceSkiaBase subclass that adds Forms input, like SilkNetGum's. It owns
// no Unity code. The host renders into an SKCanvas it owns, pushes Unity's mouse/keyboard state into
// Cursor and Keyboard each frame, then calls Update and Draw.
using Gum.Forms;
using Gum.Input;
using Gum.Wireframe;
using RenderingLibrary;
using ICursor = Gum.Wireframe.ICursor;

namespace Gum;

public class GumService : GumServiceSkiaBase, IGumService
{
    private static GumService? _default;

    /// <summary>
    /// The singleton service instance.
    /// </summary>
    public static GumService Default => _default ??= new GumService();

    /// <summary>
    /// The default cursor. The host sets its state each frame with <see cref="Input.Cursor.SetMouseState"/>.
    /// </summary>
    public Cursor Cursor => (FormsUtilities.Cursor as Cursor)!;

    /// <summary>
    /// The default keyboard. The host sets its state each frame with <see cref="Input.Keyboard.SetKeyDown"/>
    /// and <see cref="Input.Keyboard.AddTypedText"/>.
    /// </summary>
    /// <remarks>
    /// Null while a custom keyboard is installed with <see cref="FormsUtilities.SetKeyboard"/>.
    /// </remarks>
    public Keyboard Keyboard => (FormsUtilities.Keyboard as Keyboard)!;

    /// <inheritdoc/>
    ICursor? IGumService.CreateCursor() => new Cursor();

    /// <inheritdoc/>
    IInputReceiverKeyboard? IGumService.CreateKeyboard() => new Keyboard();

    /// <summary>
    /// Per-frame tick. Push this frame's input first, then call once per frame before
    /// <see cref="GumServiceSkiaBase.Draw"/> with total elapsed seconds since startup. Runs the base's
    /// deferred-queue/animation tick, then pumps Forms input (cursor/keyboard activity, control events).
    /// </summary>
    /// <param name="totalSeconds">Total elapsed time in seconds since startup.</param>
    public override void Update(double totalSeconds)
    {
        base.Update(totalSeconds);
        FormsUtilities.Update(totalSeconds, Root);
    }
}
