using Gum.Wireframe;
using System.Collections.Generic;
using System.Text;
using GumKeys = Gum.Forms.Input.Keys;

namespace Gum.Input;

/// <summary>
/// Keyboard fed by the Unity host. Each frame, before <c>GumService.Update</c>, the host reports the
/// keys held down with <see cref="SetKeyDown"/> and any typed characters with <see cref="AddTypedText"/>.
/// <see cref="Activity"/> then latches that state for the frame and derives push/release edges and key
/// repeat. Modeled on <c>Runtimes/SilkNetGum/Input/Keyboard.Silk.cs</c>.
/// </summary>
public class Keyboard : IInputReceiverKeyboard
{
    // Written by the host between frames; latched into _currentDown by Activity.
    private readonly HashSet<GumKeys> _pushedDown = new();

    private readonly HashSet<GumKeys> _currentDown = new();
    private readonly HashSet<GumKeys> _previousDown = new();

    // Unity's input has no repeat poll for non-text keys, so discrete key actions (holding an arrow key
    // to move a caret or navigate a ListBox) are timed here. Mirrors MonoGame's RepeatDelay/RepeatRate.
    private readonly Dictionary<GumKeys, double> _keyDownSince = new();
    private readonly Dictionary<GumKeys, double> _lastRepeatTime = new();
    private double _currentGameTime;

    private readonly StringBuilder _charsTyped = new();
    private string _frameChars = "";

    /// <summary>
    /// Delay after the initial key press before repeat typing begins.
    /// </summary>
    public System.TimeSpan RepeatDelay { get; set; } = System.TimeSpan.FromMilliseconds(500);

    /// <summary>
    /// Interval between repeated key-typed events while a key is held down, once
    /// <see cref="RepeatDelay"/> has elapsed.
    /// </summary>
    public System.TimeSpan RepeatRate { get; set; } = System.TimeSpan.FromMilliseconds(70);

    /// <summary>
    /// Reports whether <paramref name="key"/> is held down. Takes effect at the next <see cref="Activity"/>.
    /// </summary>
    public void SetKeyDown(GumKeys key, bool isDown)
    {
        if (isDown)
        {
            _pushedDown.Add(key);
        }
        else
        {
            _pushedDown.Remove(key);
        }
    }

    /// <summary>
    /// Appends characters the OS produced (including its own key repeat), which TextBox text entry reads
    /// through <see cref="GetStringTyped"/> on the next <see cref="Activity"/>. Control characters 0-29
    /// (Backspace, Enter, Ctrl+letter, ...) are dropped, as MonoGame's Keyboard does: TextBox handles
    /// those through the keys.
    /// </summary>
    public void AddTypedText(char character)
    {
        if (character > 29)
        {
            _charsTyped.Append(character);
        }
    }

    /// <summary>
    /// Returns true if either the left or right shift key is currently pressed down.
    /// </summary>
    public bool IsShiftDown => KeyDown(GumKeys.LeftShift) || KeyDown(GumKeys.RightShift);

    /// <summary>
    /// Returns true if either the left or right control key is currently pressed down.
    /// </summary>
    public bool IsCtrlDown => KeyDown(GumKeys.LeftControl) || KeyDown(GumKeys.RightControl);

    /// <summary>
    /// Returns true if either Command key is held on macOS, where it is reported as a Windows key. Always false
    /// elsewhere, so the Windows key never triggers text shortcuts.
    /// </summary>
    public bool IsCommandDown => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
        System.Runtime.InteropServices.OSPlatform.OSX) && (KeyDown(GumKeys.LeftWindows) || KeyDown(GumKeys.RightWindows));

    /// <summary>
    /// Returns true if either the left or right alt key is currently pressed down.
    /// </summary>
    public bool IsAltDown => KeyDown(GumKeys.LeftAlt) || KeyDown(GumKeys.RightAlt);

    /// <inheritdoc/>
    IEnumerable<GumKeys> IInputReceiverKeyboard.KeysTyped
    {
        get
        {
            foreach (GumKeys key in _currentDown)
            {
                if (KeyTyped(key))
                {
                    yield return key;
                }
            }
        }
    }

    /// <inheritdoc/>
    public bool KeyDown(GumKeys key) => _currentDown.Contains(key);

    /// <inheritdoc/>
    public bool KeyPushed(GumKeys key) => _currentDown.Contains(key) && !_previousDown.Contains(key);

    /// <inheritdoc/>
    public bool KeyReleased(GumKeys key) => !_currentDown.Contains(key) && _previousDown.Contains(key);

    /// <inheritdoc/>
    /// <remarks>
    /// Returns true on the initial press and again at <see cref="RepeatDelay"/>/<see cref="RepeatRate"/>
    /// intervals while the key is held.
    /// </remarks>
    public bool KeyTyped(GumKeys key)
    {
        if (KeyPushed(key))
        {
            return true;
        }

        if (!KeyDown(key) || !_keyDownSince.TryGetValue(key, out double downSince))
        {
            return false;
        }

        if (_currentGameTime - downSince < RepeatDelay.TotalSeconds)
        {
            return false;
        }

        if (_lastRepeatTime.TryGetValue(key, out double lastRepeat) &&
            _currentGameTime - lastRepeat < RepeatRate.TotalSeconds)
        {
            return false;
        }

        _lastRepeatTime[key] = _currentGameTime;
        return true;
    }

    /// <summary>
    /// Latches the keys and characters the host pushed since the previous frame. Called by Gum via
    /// FormsUtilities.Update.
    /// </summary>
    /// <param name="gameTime">The number of seconds since the start of the game.</param>
    public void Activity(double gameTime)
    {
        _currentGameTime = gameTime;

        _previousDown.Clear();
        _previousDown.UnionWith(_currentDown);

        _currentDown.Clear();
        _currentDown.UnionWith(_pushedDown);

        foreach (GumKeys key in _currentDown)
        {
            if (!_previousDown.Contains(key))
            {
                _keyDownSince[key] = gameTime;
                _lastRepeatTime.Remove(key);
            }
        }

        foreach (GumKeys key in _previousDown)
        {
            if (!_currentDown.Contains(key))
            {
                _keyDownSince.Remove(key);
                _lastRepeatTime.Remove(key);
            }
        }

        _frameChars = _charsTyped.ToString();
        _charsTyped.Clear();
    }

    /// <summary>
    /// Retrieves the string of Unicode characters typed since the previous <see cref="Activity"/>.
    /// </summary>
    public string GetStringTyped() => _frameChars;
}
