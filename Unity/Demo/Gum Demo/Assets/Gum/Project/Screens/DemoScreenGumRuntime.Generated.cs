//Code for DemoScreenGum
using Assembly_CSharp.Components.Meadow.Controls;
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Screens
{
    partial class DemoScreenGumRuntime : Gum.Wireframe.GraphicalUiElement
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("DemoScreenGum", typeof(DemoScreenGumRuntime));
        }
        public ContainerRuntime DemoSettingsMenu { get; protected set; }
        public RectangleRuntime Background { get; protected set; }
        public ContainerRuntime MenuTitle { get; protected set; }
        public ContainerRuntime MenuTitle1 { get; protected set; }
        public ContainerRuntime MenuItems { get; protected set; }
        public TextRuntime TitleText { get; protected set; }
        public TextRuntime TitleText1 { get; protected set; }
        public LabelRuntime ResolutionLabel { get; protected set; }
        public ListBoxRuntime ResolutionBox { get; protected set; }
        public ButtonRuntime DetectResolutionsButton { get; protected set; }
        public ButtonRuntime ShowDialogButton { get; protected set; }
        public ButtonRuntime ShowToastButton { get; protected set; }
        public CheckBoxRuntime FullScreenCheckbox { get; protected set; }
        public LabelRuntime MusicLabel { get; protected set; }
        public SliderRuntime MusicSlider { get; protected set; }
        public LabelRuntime SoundLabel { get; protected set; }
        public SliderRuntime SoundSlider { get; protected set; }
        public LabelRuntime ControlLabel { get; protected set; }
        public RadioButtonRuntime RadioButtonInstance { get; protected set; }
        public RadioButtonRuntime RadioButtonInstance1 { get; protected set; }
        public RadioButtonRuntime RadioButtonInstance2 { get; protected set; }
        public LabelRuntime DifficultyLabel { get; protected set; }
        public RectangleRuntime Background1 { get; protected set; }
        public ComboBoxRuntime ComboBoxInstance { get; protected set; }
        public ContainerRuntime ButtonContainer { get; protected set; }
        public ButtonRuntime ButtonConfirmInstance { get; protected set; }
        public ButtonRuntime ButtonDenyInstance { get; protected set; }
        public ContainerRuntime DemoDialog { get; protected set; }
        public ContainerRuntime MarginContainer { get; protected set; }
        public LabelRuntime LabelInstance { get; protected set; }
        public TextBoxRuntime TextBoxInstance { get; protected set; }
        public PasswordBoxRuntime TextBoxInstance1 { get; protected set; }
        public TextBoxRuntime MultiLineTextBox { get; protected set; }
        public DialogBoxRuntime DialogBoxInstance { get; protected set; }
        public WindowRuntime WindowStandardInstance { get; protected set; }
        public LabelRuntime LabelInstance1 { get; protected set; }

        public DemoScreenGumRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("DemoScreenGum");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            DemoSettingsMenu = this.GetGraphicalUiElementByName("DemoSettingsMenu") as global::Gum.GueDeriving.ContainerRuntime;
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            MenuTitle = this.GetGraphicalUiElementByName("MenuTitle") as global::Gum.GueDeriving.ContainerRuntime;
            MenuTitle1 = this.GetGraphicalUiElementByName("MenuTitle1") as global::Gum.GueDeriving.ContainerRuntime;
            MenuItems = this.GetGraphicalUiElementByName("MenuItems") as global::Gum.GueDeriving.ContainerRuntime;
            TitleText = this.GetGraphicalUiElementByName("TitleText") as global::Gum.GueDeriving.TextRuntime;
            TitleText1 = this.GetGraphicalUiElementByName("TitleText1") as global::Gum.GueDeriving.TextRuntime;
            ResolutionLabel = this.GetGraphicalUiElementByName("ResolutionLabel") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            ResolutionBox = this.GetGraphicalUiElementByName("ResolutionBox") as Assembly_CSharp.Components.Meadow.Controls.ListBoxRuntime;
            DetectResolutionsButton = this.GetGraphicalUiElementByName("DetectResolutionsButton") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
            ShowDialogButton = this.GetGraphicalUiElementByName("ShowDialogButton") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
            ShowToastButton = this.GetGraphicalUiElementByName("ShowToastButton") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
            FullScreenCheckbox = this.GetGraphicalUiElementByName("FullScreenCheckbox") as Assembly_CSharp.Components.Meadow.Controls.CheckBoxRuntime;
            MusicLabel = this.GetGraphicalUiElementByName("MusicLabel") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            MusicSlider = this.GetGraphicalUiElementByName("MusicSlider") as Assembly_CSharp.Components.Meadow.Controls.SliderRuntime;
            SoundLabel = this.GetGraphicalUiElementByName("SoundLabel") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            SoundSlider = this.GetGraphicalUiElementByName("SoundSlider") as Assembly_CSharp.Components.Meadow.Controls.SliderRuntime;
            ControlLabel = this.GetGraphicalUiElementByName("ControlLabel") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            RadioButtonInstance = this.GetGraphicalUiElementByName("RadioButtonInstance") as Assembly_CSharp.Components.Meadow.Controls.RadioButtonRuntime;
            RadioButtonInstance1 = this.GetGraphicalUiElementByName("RadioButtonInstance1") as Assembly_CSharp.Components.Meadow.Controls.RadioButtonRuntime;
            RadioButtonInstance2 = this.GetGraphicalUiElementByName("RadioButtonInstance2") as Assembly_CSharp.Components.Meadow.Controls.RadioButtonRuntime;
            DifficultyLabel = this.GetGraphicalUiElementByName("DifficultyLabel") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            Background1 = this.GetGraphicalUiElementByName("Background1") as global::Gum.GueDeriving.RectangleRuntime;
            ComboBoxInstance = this.GetGraphicalUiElementByName("ComboBoxInstance") as Assembly_CSharp.Components.Meadow.Controls.ComboBoxRuntime;
            ButtonContainer = this.GetGraphicalUiElementByName("ButtonContainer") as global::Gum.GueDeriving.ContainerRuntime;
            ButtonConfirmInstance = this.GetGraphicalUiElementByName("ButtonConfirmInstance") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
            ButtonDenyInstance = this.GetGraphicalUiElementByName("ButtonDenyInstance") as Assembly_CSharp.Components.Meadow.Controls.ButtonRuntime;
            DemoDialog = this.GetGraphicalUiElementByName("DemoDialog") as global::Gum.GueDeriving.ContainerRuntime;
            MarginContainer = this.GetGraphicalUiElementByName("MarginContainer") as global::Gum.GueDeriving.ContainerRuntime;
            LabelInstance = this.GetGraphicalUiElementByName("LabelInstance") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            TextBoxInstance = this.GetGraphicalUiElementByName("TextBoxInstance") as Assembly_CSharp.Components.Meadow.Controls.TextBoxRuntime;
            TextBoxInstance1 = this.GetGraphicalUiElementByName("TextBoxInstance1") as Assembly_CSharp.Components.Meadow.Controls.PasswordBoxRuntime;
            MultiLineTextBox = this.GetGraphicalUiElementByName("MultiLineTextBox") as Assembly_CSharp.Components.Meadow.Controls.TextBoxRuntime;
            DialogBoxInstance = this.GetGraphicalUiElementByName("DialogBoxInstance") as Assembly_CSharp.Components.Meadow.Controls.DialogBoxRuntime;
            WindowStandardInstance = this.GetGraphicalUiElementByName("WindowStandardInstance") as Assembly_CSharp.Components.Meadow.Controls.WindowRuntime;
            LabelInstance1 = this.GetGraphicalUiElementByName("LabelInstance1") as Assembly_CSharp.Components.Meadow.Controls.LabelRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
