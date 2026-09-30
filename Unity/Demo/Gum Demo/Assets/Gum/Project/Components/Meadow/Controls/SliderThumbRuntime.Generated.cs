//Code for Meadow/Controls/SliderThumb (Container)
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
partial class SliderThumbRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/SliderThumb", typeof(SliderThumbRuntime));
    }
    public global::Gum.Forms.Controls.Button FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.Button;
    public enum ButtonCategory
    {
        Enabled,
        Highlighted,
        Pushed,
        Focused,
        HighlightedFocused,
        Disabled,
        DisabledFocused,
    }

    ButtonCategory? _buttonCategoryState;
    public ButtonCategory? ButtonCategoryState
    {
        get => _buttonCategoryState;
        set
        {
            _buttonCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("ButtonCategory"))
                {
                    var category = Categories["ButtonCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ButtonCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public CircleRuntime Body { get; protected set; }
    public CircleRuntime FocusedIndicator { get; protected set; }

    public SliderThumbRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/SliderThumb");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.Button(this);
        }
        Body = this.GetGraphicalUiElementByName("Body") as global::MonoGameGum.GueDeriving.CircleRuntime;
        FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::MonoGameGum.GueDeriving.CircleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
