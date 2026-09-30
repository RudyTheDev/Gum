//Code for Meadow/Controls/Expander (Container)
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
partial class ExpanderRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/Expander", typeof(ExpanderRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.Expander)] = typeof(ExpanderRuntime);
    }
    public global::Gum.Forms.Controls.Expander FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.Expander;
    public enum ExpanderCategory
    {
        Collapsed,
        Expanded,
        DisabledCollapsed,
        DisabledExpanded,
    }

    ExpanderCategory? _expanderCategoryState;
    public ExpanderCategory? ExpanderCategoryState
    {
        get => _expanderCategoryState;
        set
        {
            _expanderCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("ExpanderCategory"))
                {
                    var category = Categories["ExpanderCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ExpanderCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public ContainerRuntime HeaderContainer { get; protected set; }
    public RectangleRuntime HeaderBackground { get; protected set; }
    public TextRuntime ArrowIndicator { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }
    public ContainerRuntime ContentContainer { get; protected set; }

    public string Header
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public ExpanderRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/Expander");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.Expander(this);
        }
        HeaderContainer = this.GetGraphicalUiElementByName("HeaderContainer") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        HeaderBackground = this.GetGraphicalUiElementByName("HeaderBackground") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        ArrowIndicator = this.GetGraphicalUiElementByName("ArrowIndicator") as global::MonoGameGum.GueDeriving.TextRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        ContentContainer = this.GetGraphicalUiElementByName("ContentContainer") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
