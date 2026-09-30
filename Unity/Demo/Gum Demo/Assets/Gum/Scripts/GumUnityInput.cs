using System;
using System.Collections.Generic;
using Gum;
using UnityEngine;
using UnityEngine.InputSystem;
using GumKeys = Gum.Forms.Input.Keys;
using UnityKey = UnityEngine.InputSystem.Key;

/// <summary>
/// Reads Unity's Input System each frame and pushes it into Gum's cursor and keyboard. Call
/// <see cref="Push"/> once per frame before <c>GumService.Update</c>.
/// </summary>
public sealed class GumUnityInput : IDisposable
{
    // Gum's keys follow XNA's key space. Several Unity keys can map to one Gum key (both Enters);
    // Gum keys with no Unity counterpart (media, browser, IME keys, F13-F24) are left out.
    static readonly (GumKeys gum, UnityKey unity)[] KeyMap =
    {
        (GumKeys.Back, UnityKey.Backspace),
        (GumKeys.Tab, UnityKey.Tab),
        (GumKeys.Enter, UnityKey.Enter),
        (GumKeys.Enter, UnityKey.NumpadEnter),
        (GumKeys.Pause, UnityKey.Pause),
        (GumKeys.CapsLock, UnityKey.CapsLock),
        (GumKeys.Escape, UnityKey.Escape),
        (GumKeys.Space, UnityKey.Space),
        (GumKeys.PageUp, UnityKey.PageUp),
        (GumKeys.PageDown, UnityKey.PageDown),
        (GumKeys.End, UnityKey.End),
        (GumKeys.Home, UnityKey.Home),
        (GumKeys.Left, UnityKey.LeftArrow),
        (GumKeys.Up, UnityKey.UpArrow),
        (GumKeys.Right, UnityKey.RightArrow),
        (GumKeys.Down, UnityKey.DownArrow),
        (GumKeys.PrintScreen, UnityKey.PrintScreen),
        (GumKeys.Insert, UnityKey.Insert),
        (GumKeys.Delete, UnityKey.Delete),

        (GumKeys.D0, UnityKey.Digit0),
        (GumKeys.D1, UnityKey.Digit1),
        (GumKeys.D2, UnityKey.Digit2),
        (GumKeys.D3, UnityKey.Digit3),
        (GumKeys.D4, UnityKey.Digit4),
        (GumKeys.D5, UnityKey.Digit5),
        (GumKeys.D6, UnityKey.Digit6),
        (GumKeys.D7, UnityKey.Digit7),
        (GumKeys.D8, UnityKey.Digit8),
        (GumKeys.D9, UnityKey.Digit9),

        (GumKeys.A, UnityKey.A),
        (GumKeys.B, UnityKey.B),
        (GumKeys.C, UnityKey.C),
        (GumKeys.D, UnityKey.D),
        (GumKeys.E, UnityKey.E),
        (GumKeys.F, UnityKey.F),
        (GumKeys.G, UnityKey.G),
        (GumKeys.H, UnityKey.H),
        (GumKeys.I, UnityKey.I),
        (GumKeys.J, UnityKey.J),
        (GumKeys.K, UnityKey.K),
        (GumKeys.L, UnityKey.L),
        (GumKeys.M, UnityKey.M),
        (GumKeys.N, UnityKey.N),
        (GumKeys.O, UnityKey.O),
        (GumKeys.P, UnityKey.P),
        (GumKeys.Q, UnityKey.Q),
        (GumKeys.R, UnityKey.R),
        (GumKeys.S, UnityKey.S),
        (GumKeys.T, UnityKey.T),
        (GumKeys.U, UnityKey.U),
        (GumKeys.V, UnityKey.V),
        (GumKeys.W, UnityKey.W),
        (GumKeys.X, UnityKey.X),
        (GumKeys.Y, UnityKey.Y),
        (GumKeys.Z, UnityKey.Z),

        (GumKeys.LeftWindows, UnityKey.LeftMeta),
        (GumKeys.RightWindows, UnityKey.RightMeta),
        (GumKeys.Apps, UnityKey.ContextMenu),

        (GumKeys.NumPad0, UnityKey.Numpad0),
        (GumKeys.NumPad1, UnityKey.Numpad1),
        (GumKeys.NumPad2, UnityKey.Numpad2),
        (GumKeys.NumPad3, UnityKey.Numpad3),
        (GumKeys.NumPad4, UnityKey.Numpad4),
        (GumKeys.NumPad5, UnityKey.Numpad5),
        (GumKeys.NumPad6, UnityKey.Numpad6),
        (GumKeys.NumPad7, UnityKey.Numpad7),
        (GumKeys.NumPad8, UnityKey.Numpad8),
        (GumKeys.NumPad9, UnityKey.Numpad9),
        (GumKeys.Multiply, UnityKey.NumpadMultiply),
        (GumKeys.Add, UnityKey.NumpadPlus),
        (GumKeys.Subtract, UnityKey.NumpadMinus),
        (GumKeys.Decimal, UnityKey.NumpadPeriod),
        (GumKeys.Divide, UnityKey.NumpadDivide),

        (GumKeys.F1, UnityKey.F1),
        (GumKeys.F2, UnityKey.F2),
        (GumKeys.F3, UnityKey.F3),
        (GumKeys.F4, UnityKey.F4),
        (GumKeys.F5, UnityKey.F5),
        (GumKeys.F6, UnityKey.F6),
        (GumKeys.F7, UnityKey.F7),
        (GumKeys.F8, UnityKey.F8),
        (GumKeys.F9, UnityKey.F9),
        (GumKeys.F10, UnityKey.F10),
        (GumKeys.F11, UnityKey.F11),
        (GumKeys.F12, UnityKey.F12),

        (GumKeys.NumLock, UnityKey.NumLock),
        (GumKeys.Scroll, UnityKey.ScrollLock),
        (GumKeys.LeftShift, UnityKey.LeftShift),
        (GumKeys.RightShift, UnityKey.RightShift),
        (GumKeys.LeftControl, UnityKey.LeftCtrl),
        (GumKeys.RightControl, UnityKey.RightCtrl),
        (GumKeys.LeftAlt, UnityKey.LeftAlt),
        (GumKeys.RightAlt, UnityKey.RightAlt),

        (GumKeys.OemSemicolon, UnityKey.Semicolon),
        (GumKeys.OemPlus, UnityKey.Equals),
        (GumKeys.OemComma, UnityKey.Comma),
        (GumKeys.OemMinus, UnityKey.Minus),
        (GumKeys.OemPeriod, UnityKey.Period),
        (GumKeys.OemQuestion, UnityKey.Slash),
        (GumKeys.OemTilde, UnityKey.Backquote),
        (GumKeys.OemOpenBrackets, UnityKey.LeftBracket),
        (GumKeys.OemPipe, UnityKey.Backslash),
        (GumKeys.OemCloseBrackets, UnityKey.RightBracket),
        (GumKeys.OemQuotes, UnityKey.Quote),
        (GumKeys.OemBackslash, UnityKey.OEM1),
    };

    readonly GumService _gum;
    readonly HashSet<GumKeys> _downThisFrame = new HashSet<GumKeys>();
    Keyboard _subscribedKeyboard;
    float _scrollNotches;

    public GumUnityInput(GumService gum)
    {
        _gum = gum;
        _gum.Cursor.IsMobile = Application.isMobilePlatform;
    }

    /// <summary>
    /// Pushes this frame's mouse and keyboard state into Gum.
    /// </summary>
    /// <param name="canvasHeight">The Gum canvas height in pixels, to flip Unity's bottom-up screen Y.</param>
    public void Push(int canvasHeight)
    {
        PushMouse(canvasHeight);
        PushKeyboard();
    }

    public void Dispose()
    {
        if (_subscribedKeyboard != null)
        {
            _subscribedKeyboard.onTextInput -= HandleTextInput;
            _subscribedKeyboard = null;
        }
    }

    void PushMouse(int canvasHeight)
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
            return;

        Vector2 position = mouse.position.ReadValue();

        // The Input System's default scroll delta is 1 per wheel notch; Gum keeps XNA's running total of 120 per notch.
        _scrollNotches += mouse.scroll.ReadValue().y;

        _gum.Cursor.SetMouseState(
            (int)position.x,
            canvasHeight - 1 - (int)position.y,
            mouse.leftButton.isPressed,
            mouse.middleButton.isPressed,
            mouse.rightButton.isPressed,
            (int)(_scrollNotches * 120));
    }

    void PushKeyboard()
    {
        Keyboard keyboard = Keyboard.current;

        // The current keyboard can change (device connected/removed), so move the text subscription with it.
        if (keyboard != _subscribedKeyboard)
        {
            Dispose();
            if (keyboard != null)
            {
                keyboard.onTextInput += HandleTextInput;
                _subscribedKeyboard = keyboard;
            }
        }

        _downThisFrame.Clear();
        if (keyboard != null)
        {
            foreach (var (gum, unity) in KeyMap)
            {
                if (keyboard[unity].isPressed)
                    _downThisFrame.Add(gum);
            }
        }

        foreach (var (gum, _) in KeyMap)
        {
            _gum.Keyboard.SetKeyDown(gum, _downThisFrame.Contains(gum));
        }
    }

    void HandleTextInput(char character) => _gum.Keyboard.AddTypedText(character);
}
