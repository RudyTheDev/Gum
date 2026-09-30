//Code for Meadow/Controls/ColorPicker (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Components.Meadow.Controls;
partial class ColorPickerRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ColorPicker", typeof(ColorPickerRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.ColorPicker)] = typeof(ColorPickerRuntime);
    }
    public global::Gum.Forms.Controls.ColorPicker FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ColorPicker;
    public ContainerRuntime SaturationValueContainer { get; protected set; }
    public SpriteRuntime SaturationValueDisplay { get; protected set; }
    public RectangleRuntime SaturationValueOutline { get; protected set; }
    public ContainerRuntime SaturationValueIndicator { get; protected set; }
    public RectangleRuntime SaturationValueIndicatorOuter { get; protected set; }
    public RectangleRuntime SaturationValueIndicatorInner { get; protected set; }
    public ContainerRuntime HueContainer { get; protected set; }
    public SpriteRuntime HueDisplay { get; protected set; }
    public RectangleRuntime HueOutline { get; protected set; }
    public ContainerRuntime HueIndicator { get; protected set; }
    public RectangleRuntime HueIndicatorOuter { get; protected set; }
    public RectangleRuntime HueIndicatorInner { get; protected set; }

    public ColorPickerRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ColorPicker");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.ColorPicker(this);
        }
        SaturationValueContainer = this.GetGraphicalUiElementByName("SaturationValueContainer") as global::Gum.GueDeriving.ContainerRuntime;
        SaturationValueDisplay = this.GetGraphicalUiElementByName("SaturationValueDisplay") as global::Gum.GueDeriving.SpriteRuntime;
        SaturationValueOutline = this.GetGraphicalUiElementByName("SaturationValueOutline") as global::Gum.GueDeriving.RectangleRuntime;
        SaturationValueIndicator = this.GetGraphicalUiElementByName("SaturationValueIndicator") as global::Gum.GueDeriving.ContainerRuntime;
        SaturationValueIndicatorOuter = this.GetGraphicalUiElementByName("SaturationValueIndicatorOuter") as global::Gum.GueDeriving.RectangleRuntime;
        SaturationValueIndicatorInner = this.GetGraphicalUiElementByName("SaturationValueIndicatorInner") as global::Gum.GueDeriving.RectangleRuntime;
        HueContainer = this.GetGraphicalUiElementByName("HueContainer") as global::Gum.GueDeriving.ContainerRuntime;
        HueDisplay = this.GetGraphicalUiElementByName("HueDisplay") as global::Gum.GueDeriving.SpriteRuntime;
        HueOutline = this.GetGraphicalUiElementByName("HueOutline") as global::Gum.GueDeriving.RectangleRuntime;
        HueIndicator = this.GetGraphicalUiElementByName("HueIndicator") as global::Gum.GueDeriving.ContainerRuntime;
        HueIndicatorOuter = this.GetGraphicalUiElementByName("HueIndicatorOuter") as global::Gum.GueDeriving.RectangleRuntime;
        HueIndicatorInner = this.GetGraphicalUiElementByName("HueIndicatorInner") as global::Gum.GueDeriving.RectangleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
