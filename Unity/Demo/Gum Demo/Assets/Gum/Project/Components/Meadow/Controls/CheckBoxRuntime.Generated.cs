//Code for Meadow/Controls/CheckBox (Container)
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
partial class CheckBoxRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/CheckBox", typeof(CheckBoxRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.CheckBox)] = typeof(CheckBoxRuntime);
    }
    public global::Gum.Forms.Controls.CheckBox FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.CheckBox;
    public enum CheckBoxCategory
    {
        EnabledOff,
        HighlightedOff,
        FocusedOff,
        HighlightedFocusedOff,
        PushedOff,
        DisabledOff,
        DisabledFocusedOff,
        EnabledOn,
        HighlightedOn,
        FocusedOn,
        HighlightedFocusedOn,
        PushedOn,
        DisabledOn,
        DisabledFocusedOn,
        EnabledIndeterminate,
        HighlightedIndeterminate,
        FocusedIndeterminate,
        HighlightedFocusedIndeterminate,
        PushedIndeterminate,
        DisabledIndeterminate,
        DisabledFocusedIndeterminate,
    }

    CheckBoxCategory? _checkBoxCategoryState;
    public CheckBoxCategory? CheckBoxCategoryState
    {
        get => _checkBoxCategoryState;
        set
        {
            _checkBoxCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("CheckBoxCategory"))
                {
                    var category = Categories["CheckBoxCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "CheckBoxCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public RectangleRuntime CheckBoxBackground { get; protected set; }
    public RectangleRuntime BoxBorder { get; protected set; }
    public TextRuntime InnerCheck { get; protected set; }
    public RectangleRuntime DashIndicator { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public RectangleRuntime FocusedIndicator { get; protected set; }

    public string Text
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public CheckBoxRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/CheckBox");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.CheckBox(this);
        }
        CheckBoxBackground = this.GetGraphicalUiElementByName("CheckBoxBackground") as global::Gum.GueDeriving.RectangleRuntime;
        BoxBorder = this.GetGraphicalUiElementByName("BoxBorder") as global::Gum.GueDeriving.RectangleRuntime;
        InnerCheck = this.GetGraphicalUiElementByName("InnerCheck") as global::Gum.GueDeriving.TextRuntime;
        DashIndicator = this.GetGraphicalUiElementByName("DashIndicator") as global::Gum.GueDeriving.RectangleRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::Gum.GueDeriving.TextRuntime;
        FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::Gum.GueDeriving.RectangleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
