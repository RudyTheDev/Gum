//Code for Meadow/Styles (Container)
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Components.Meadow;
partial class StylesRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Styles", typeof(StylesRuntime));
    }
    public ContainerRuntime Colors { get; protected set; }
    public RectangleRuntime White { get; protected set; }
    public RectangleRuntime Cream { get; protected set; }
    public RectangleRuntime Cream2 { get; protected set; }
    public RectangleRuntime PeachDark { get; protected set; }
    public RectangleRuntime PeachLight { get; protected set; }
    public RectangleRuntime Sage { get; protected set; }
    public RectangleRuntime SageDark { get; protected set; }
    public RectangleRuntime Teal { get; protected set; }
    public RectangleRuntime TealDark { get; protected set; }
    public RectangleRuntime Blue { get; protected set; }
    public RectangleRuntime BlueDark { get; protected set; }
    public RectangleRuntime BlueHover { get; protected set; }
    public RectangleRuntime Coral { get; protected set; }
    public RectangleRuntime CoralDark { get; protected set; }
    public RectangleRuntime Muted { get; protected set; }
    public RectangleRuntime Disabled { get; protected set; }
    public RectangleRuntime DisabledInk { get; protected set; }
    public RectangleRuntime DisabledSliderFill { get; protected set; }
    public RectangleRuntime SageFocusRing { get; protected set; }
    public RectangleRuntime BlueFocusRing { get; protected set; }
    public RectangleRuntime ThumbShadow { get; protected set; }
    public RectangleRuntime WindowShadow { get; protected set; }
    public TextRuntime Normal { get; protected set; }
    public TextRuntime Strong { get; protected set; }
    public TextRuntime Body { get; protected set; }
    public ContainerRuntime TextStyles { get; protected set; }

    public StylesRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Styles");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        Colors = this.GetGraphicalUiElementByName("Colors") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        White = this.GetGraphicalUiElementByName("White") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Cream = this.GetGraphicalUiElementByName("Cream") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Cream2 = this.GetGraphicalUiElementByName("Cream2") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        PeachDark = this.GetGraphicalUiElementByName("PeachDark") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        PeachLight = this.GetGraphicalUiElementByName("PeachLight") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Sage = this.GetGraphicalUiElementByName("Sage") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        SageDark = this.GetGraphicalUiElementByName("SageDark") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Teal = this.GetGraphicalUiElementByName("Teal") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        TealDark = this.GetGraphicalUiElementByName("TealDark") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Blue = this.GetGraphicalUiElementByName("Blue") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        BlueDark = this.GetGraphicalUiElementByName("BlueDark") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        BlueHover = this.GetGraphicalUiElementByName("BlueHover") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Coral = this.GetGraphicalUiElementByName("Coral") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        CoralDark = this.GetGraphicalUiElementByName("CoralDark") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Muted = this.GetGraphicalUiElementByName("Muted") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Disabled = this.GetGraphicalUiElementByName("Disabled") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        DisabledInk = this.GetGraphicalUiElementByName("DisabledInk") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        DisabledSliderFill = this.GetGraphicalUiElementByName("DisabledSliderFill") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        SageFocusRing = this.GetGraphicalUiElementByName("SageFocusRing") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        BlueFocusRing = this.GetGraphicalUiElementByName("BlueFocusRing") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        ThumbShadow = this.GetGraphicalUiElementByName("ThumbShadow") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        WindowShadow = this.GetGraphicalUiElementByName("WindowShadow") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        Normal = this.GetGraphicalUiElementByName("Normal") as global::MonoGameGum.GueDeriving.TextRuntime;
        Strong = this.GetGraphicalUiElementByName("Strong") as global::MonoGameGum.GueDeriving.TextRuntime;
        Body = this.GetGraphicalUiElementByName("Body") as global::MonoGameGum.GueDeriving.TextRuntime;
        TextStyles = this.GetGraphicalUiElementByName("TextStyles") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
