using System.Collections.ObjectModel;
using Gum.Mvvm;

/// <summary>
/// View model for the demo screen, the same as Samples/GumFormsSample's DemoScreenViewModel: the
/// difficulty and resolution lists, and the control scheme the radio buttons pick.
/// </summary>
public class DemoScreenViewModel : ViewModel
{
    public enum Scheme
    {
        KeyboardAndMouse,
        Gamepad,
        Touchscreen
    }

    public ObservableCollection<string> ComboBoxItems
    {
        get => Get<ObservableCollection<string>>();
        set => Set(value);
    }

    public ObservableCollection<string> ListBoxItems
    {
        get => Get<ObservableCollection<string>>();
        set => Set(value);
    }

    public Scheme ControlScheme
    {
        get => Get<Scheme>();
        set => Set(value);
    }

    [DependsOn(nameof(ControlScheme))]
    public bool IsKeyboardAndMouseChecked
    {
        get => ControlScheme == Scheme.KeyboardAndMouse;
        set
        {
            if (value) ControlScheme = Scheme.KeyboardAndMouse;
        }
    }

    [DependsOn(nameof(ControlScheme))]
    public bool IsGamepadChecked
    {
        get => ControlScheme == Scheme.Gamepad;
        set
        {
            if (value) ControlScheme = Scheme.Gamepad;
        }
    }

    [DependsOn(nameof(ControlScheme))]
    public bool IsTouchscreenChecked
    {
        get => ControlScheme == Scheme.Touchscreen;
        set
        {
            if (value) ControlScheme = Scheme.Touchscreen;
        }
    }

    public DemoScreenViewModel()
    {
        ComboBoxItems = new ObservableCollection<string> { "Easy", "Medium", "Hard", "Impossible" };

        ListBoxItems = new ObservableCollection<string>
        {
            "400x300", "600x800", "1024x768", "1280x720", "1920x1080", "2560x1440", "3840x2160", "7680x4320"
        };

        ControlScheme = Scheme.KeyboardAndMouse;
    }
}
