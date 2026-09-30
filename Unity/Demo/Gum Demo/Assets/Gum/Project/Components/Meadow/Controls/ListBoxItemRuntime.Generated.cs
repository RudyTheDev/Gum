//Code for Meadow/Controls/ListBoxItem (Container)
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
partial class ListBoxItemRuntime : ContainerRuntime
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ListBoxItem", typeof(ListBoxItemRuntime));
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.ListBoxItem)] = typeof(ListBoxItemRuntime);
    }
    public global::Gum.Forms.Controls.ListBoxItem FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ListBoxItem;
    public enum ListBoxItemCategory
    {
        Enabled,
        Highlighted,
        Selected,
        Focused,
        Disabled,
    }

    ListBoxItemCategory? _listBoxItemCategoryState;
    public ListBoxItemCategory? ListBoxItemCategoryState
    {
        get => _listBoxItemCategoryState;
        set
        {
            _listBoxItemCategoryState = value;
            if(value != null)
            {
                if(Categories.ContainsKey("ListBoxItemCategory"))
                {
                    var category = Categories["ListBoxItemCategory"];
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
                else
                {
                    var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ListBoxItemCategory");
                    var state = category.States.Find(item => item.Name == value.ToString());
                    this.ApplyState(state);
                }
            }
        }
    }
    public RectangleRuntime Background { get; protected set; }
    public TextRuntime TextInstance { get; protected set; }

    public string ListItemDisplayText
    {
        get => TextInstance.Text;
        set => TextInstance.Text = value;
    }

    public ListBoxItemRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
    {
        if(fullInstantiation)
        {
            var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ListBoxItem");
            element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
        }



    }
    public override void AfterFullCreation()
    {
        if (FormsControl == null)
        {
            FormsControlAsObject = new global::Gum.Forms.Controls.ListBoxItem(this);
        }
        Background = this.GetGraphicalUiElementByName("Background") as global::MonoGameGum.GueDeriving.RectangleRuntime;
        TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::MonoGameGum.GueDeriving.TextRuntime;
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}
