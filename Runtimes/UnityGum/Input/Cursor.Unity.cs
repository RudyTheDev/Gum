namespace Gum.Input;

/// <summary>
/// Unity half of the shared <see cref="Cursor"/> partial. Unity input is read by the host, which
/// pushes it here once per frame with <see cref="SetMouseState"/> (and <see cref="SetTouches"/> on
/// touch devices) before calling <c>GumService.Update</c>. Mirrors <c>Cursor.Silk.cs</c>.
/// </summary>
public partial class Cursor
{
    private MouseState _pushedMouseState;
    private TouchCollection _pushedTouches = new TouchCollection();

    /// <summary>
    /// Whether the device is a phone or tablet, where only touches are read (the mouse state is ignored).
    /// The host sets this from <c>Application.isMobilePlatform</c>.
    /// </summary>
    public bool IsMobile { get; set; }

    /// <summary>
    /// Sets the mouse state the next <see cref="Activity(double)"/> reads.
    /// </summary>
    /// <param name="x">Mouse X in canvas pixels, from the left edge.</param>
    /// <param name="y">Mouse Y in canvas pixels, from the top edge (Unity's screen Y is from the bottom).</param>
    /// <param name="leftDown">Whether the left button is held.</param>
    /// <param name="middleDown">Whether the middle button is held.</param>
    /// <param name="rightDown">Whether the right button is held.</param>
    /// <param name="scrollWheelValue">
    /// The running scroll total in XNA units: 120 per wheel notch, accumulated since startup.
    /// </param>
    public void SetMouseState(int x, int y, bool leftDown, bool middleDown, bool rightDown, int scrollWheelValue)
    {
        _pushedMouseState = new MouseState
        {
            X = x,
            Y = y,
            LeftButton = leftDown ? ButtonState.Pressed : ButtonState.Released,
            MiddleButton = middleDown ? ButtonState.Pressed : ButtonState.Released,
            RightButton = rightDown ? ButtonState.Pressed : ButtonState.Released,
            ScrollWheelValue = scrollWheelValue,
        };
    }

    /// <summary>
    /// Sets the touches the next <see cref="Activity(double)"/> reads, in canvas pixels from the top left.
    /// </summary>
    public void SetTouches(TouchCollection touches) => _pushedTouches = touches;

    private MouseState GetMouseState() => _pushedMouseState;

    private TouchCollection GetTouchCollection() => _pushedTouches;

    private int? GetViewportLeft() => 0;

    private int? GetViewportTop() => 0;
}
