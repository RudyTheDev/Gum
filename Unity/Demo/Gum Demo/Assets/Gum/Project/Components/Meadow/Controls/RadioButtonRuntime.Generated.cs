//Code for Meadow/Controls/RadioButton (Container)
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
partial class RadioButtonRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/RadioButton", typeof(RadioButtonRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.RadioButton)] = typeof(RadioButtonRuntime);
    }
    public global::Gum.Forms.Controls.RadioButton FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.RadioButton;
    public enum RadioButtonCategory
    {
        EnabledOn,
        EnabledOff,
        DisabledOn,
        DisabledOff,
        HighlightedOn,
        HighlightedOff,
        PushedOn,
        PushedOff,
        FocusedOn,
        FocusedOff,
        HighlightedFocusedOn,
        HighlightedFocusedOff,
        DisabledFocusedOn,
        DisabledFocusedOff,
    }

    RadioButtonCategory? _radioButtonCategoryState;
    public RadioButtonCategory? RadioButtonCategoryState
    {
        get => _radioButtonCategoryState;
        set
        {
            _radioButtonCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("RadioButtonCategory"))
                {
                    var category = Categories["RadioButtonCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "RadioButtonCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public CircleRuntime RadioBackground { get; protected set; }
    public CircleRuntime Radio { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public CircleRuntime FocusedIndicator { get; protected set; }

    public string Text
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public RadioButtonRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/RadioButton");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.RadioButton(this);
        }
        RadioBackground = this.GetGraphicalUiElementByName("RadioBackground") as global::MonoGameGum.GueDeriving.CircleRuntime;
        Radio = this.GetGraphicalUiElementByName("Radio") as global::MonoGameGum.GueDeriving.CircleRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::MonoGameGum.GueDeriving.CircleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
