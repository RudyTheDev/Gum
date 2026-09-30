using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using System.Threading.Tasks;
using Assembly_CSharp.Components.Meadow.Controls;
using Gum.Forms.Controls;
using Gum.Forms.Controls.Games;
using UnityEngine;

namespace Assembly_CSharp.Screens
{
    partial class DemoScreenGumRuntime
    {
        DemoScreenViewModel _viewModel;
        DialogBox _dialogBox;

        // Wires the demo screen's controls so clicking, typing and dragging can be checked in Unity. The
        // bindings and slider setup follow Samples/GumFormsSample's DemoScreenGumRuntime; the Meadow
        // theme's extra buttons get handlers of their own. Every interaction logs to the Unity console.
        partial void CustomInitialize()
        {
            _viewModel = new DemoScreenViewModel();
            BindingContext = _viewModel;

            ComboBoxInstance.FormsControl.SetBinding(nameof(ComboBox.Items), nameof(_viewModel.ComboBoxItems));
            ResolutionBox.FormsControl.SetBinding(nameof(ListBox.Items), nameof(_viewModel.ListBoxItems));

            RadioButtonInstance.FormsControl.SetBinding(nameof(RadioButton.IsChecked), nameof(_viewModel.IsKeyboardAndMouseChecked));
            RadioButtonInstance1.FormsControl.SetBinding(nameof(RadioButton.IsChecked), nameof(_viewModel.IsGamepadChecked));
            RadioButtonInstance2.FormsControl.SetBinding(nameof(RadioButton.IsChecked), nameof(_viewModel.IsTouchscreenChecked));
            _viewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(_viewModel.ControlScheme))
                    Debug.Log($"Gum: control scheme {_viewModel.ControlScheme}");
            };

            MusicSlider.FormsControl.LargeChange = 10;
            MusicSlider.FormsControl.ValueChangeCompleted += (_, _) => Debug.Log($"Gum: music volume {MusicSlider.FormsControl.Value:0}");
            SoundSlider.FormsControl.ValueChangeCompleted += (_, _) => Debug.Log($"Gum: sound volume {SoundSlider.FormsControl.Value:0}");

            FullScreenCheckbox.FormsControl.Checked += (_, _) => Debug.Log("Gum: fullscreen checked");
            FullScreenCheckbox.FormsControl.Unchecked += (_, _) => Debug.Log("Gum: fullscreen unchecked");

            ResolutionBox.FormsControl.SelectionChanged += (_, _) => Debug.Log($"Gum: resolution {ResolutionBox.FormsControl.SelectedObject}");
            ComboBoxInstance.FormsControl.SelectionChanged += (_, _) => Debug.Log($"Gum: difficulty {ComboBoxInstance.FormsControl.SelectedObject}");

            DetectResolutionsButton.FormsControl.Click += (_, _) => DetectResolutions();

            ButtonConfirmInstance.FormsControl.Click += (_, _) => ShowToast("Confirm clicked");
            ButtonDenyInstance.FormsControl.Click += (_, _) => ShowToast("Deny clicked");
            ShowToastButton.FormsControl.Click += (_, _) => ShowToast("This is a toast");

            // No runtime creates a Forms DialogBox from DialogBoxBehavior, so wrap the visual in one here.
            _dialogBox = new DialogBox(DialogBoxInstance);
            _dialogBox.FinishedShowing += (_, _) => Debug.Log("Gum: dialog finished");
            ShowDialogButton.FormsControl.Click += (_, _) =>
            {
                Debug.Log("Gum: showing dialog");
                _dialogBox.Show(new[]
                {
                    "This is a dialog box. Its text types out one letter at a time.",
                    "Click the dialog box to skip the typing, or to go to the next page.",
                    "This is the last page. Click once more to close it."
                });
            };

            TextBoxInstance.FormsControl.TextChanged += (_, _) => Debug.Log($"Gum: text box \"{TextBoxInstance.FormsControl.Text}\"");
        }

        // Replaces the sample's hardcoded list with the resolutions Unity reports for this display.
        void DetectResolutions()
        {
            var detected = Screen.resolutions
                .Select(resolution => $"{resolution.width}x{resolution.height}")
                .Distinct()
                .ToList();

            _viewModel.ListBoxItems.Clear();
            foreach (var resolution in detected)
                _viewModel.ListBoxItems.Add(resolution);

            Debug.Log($"Gum: detected {detected.Count} resolutions");
        }

        // Shows a Meadow Toast at the bottom center of the popup layer for two seconds.
        async void ShowToast(string text)
        {
            Debug.Log($"Gum: toast \"{text}\"");

            var toast = new ToastRuntime();
            toast.TextInstance.Text = text;
            toast.XUnits = GeneralUnitType.PixelsFromMiddle;
            toast.XOrigin = HorizontalAlignment.Center;
            toast.X = 0;
            toast.YUnits = GeneralUnitType.PixelsFromLarge;
            toast.YOrigin = VerticalAlignment.Bottom;
            toast.Y = -40;
            FrameworkElement.PopupRoot.Children.Add(toast);

            await Task.Delay(2000);

            FrameworkElement.PopupRoot.Children.Remove(toast);
        }
    }
}
