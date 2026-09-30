//Code for Meadow/Controls/PasswordBox (Container)
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
partial class PasswordBoxRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/PasswordBox", typeof(PasswordBoxRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.PasswordBox)] = typeof(PasswordBoxRuntime);
    }
    public global::Gum.Forms.Controls.PasswordBox FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.PasswordBox;
    public enum PasswordBoxCategory
    {
        Enabled,
        Disabled,
        Highlighted,
        Selected,
    }

    PasswordBoxCategory? _passwordBoxCategoryState;
    public PasswordBoxCategory? PasswordBoxCategoryState
    {
        get => _passwordBoxCategoryState;
        set
        {
            _passwordBoxCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("PasswordBoxCategory"))
                {
                    var category = Categories["PasswordBoxCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "PasswordBoxCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public RectangleRuntime FocusedIndicator { get; protected set; }
    public RectangleRuntime Background { get; protected set; }
    public ContainerRuntime ClipContainer { get; protected set; }
    public RectangleRuntime SelectionInstance { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public TextRuntime PlaceholderTextInstance { get; protected set; }
    public RectangleRuntime CaretInstance { get; protected set; }
    public RectangleRuntime BorderInstance { get; protected set; }

    public string Placeholder
    {
        get => PlaceholderTextInstance.Text;
        set => PlaceholderTextInstance.Text = value;
    }

    public PasswordBoxRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/PasswordBox");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.PasswordBox(this);
        }
        FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::Gum.GueDeriving.RectangleRuntime;
        Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
        ClipContainer = this.GetGraphicalUiElementByName("ClipContainer") as global::Gum.GueDeriving.ContainerRuntime;
        SelectionInstance = this.GetGraphicalUiElementByName("SelectionInstance") as global::Gum.GueDeriving.RectangleRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::Gum.GueDeriving.TextRuntime;
        PlaceholderTextInstance = this.GetGraphicalUiElementByName("PlaceholderTextInstance") as global::Gum.GueDeriving.TextRuntime;
        CaretInstance = this.GetGraphicalUiElementByName("CaretInstance") as global::Gum.GueDeriving.RectangleRuntime;
        BorderInstance = this.GetGraphicalUiElementByName("BorderInstance") as global::Gum.GueDeriving.RectangleRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
