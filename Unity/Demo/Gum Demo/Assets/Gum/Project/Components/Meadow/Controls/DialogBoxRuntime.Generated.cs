//Code for Meadow/Controls/DialogBox (Container)
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Components.Meadow.Controls;
partial class DialogBoxRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/DialogBox", typeof(DialogBoxRuntime));
    }
    public RectangleRuntime NineSliceInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public TextRuntime ContinueIndicatorInstance { get; protected set; }

    public DialogBoxRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/DialogBox");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        NineSliceInstance = this.GetGraphicalUiElementByName("NineSliceInstance") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        ContinueIndicatorInstance = this.GetGraphicalUiElementByName("ContinueIndicatorInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
