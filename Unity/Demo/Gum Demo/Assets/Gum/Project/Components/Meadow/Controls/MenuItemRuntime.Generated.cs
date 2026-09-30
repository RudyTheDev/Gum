//Code for Meadow/Controls/MenuItem (Container)
using Gum;
using Gum.Converters;
using Gum.DataTypes;
using Gum.GueDeriving;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary.Graphics;
using System.Linq;
namespace Assembly_CSharp.Components.Meadow.Controls
{
    partial class MenuItemRuntime : ContainerRuntime
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/MenuItem", typeof(MenuItemRuntime));
            global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.MenuItem)] = typeof(MenuItemRuntime);
        }
        public global::Gum.Forms.Controls.MenuItem FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.MenuItem;
        public enum MenuItemCategory
        {
            Enabled,
            Highlighted,
            Selected,
            Focused,
            Disabled,
        }

        MenuItemCategory? _menuItemCategoryState;
        public MenuItemCategory? MenuItemCategoryState
        {
            get => _menuItemCategoryState;
            set
            {
                _menuItemCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("MenuItemCategory"))
                    {
                        var category = Categories["MenuItemCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "MenuItemCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }
        public RectangleRuntime Background { get; protected set; }
        public TextRuntime TextInstance { get; protected set; }
        public TextRuntime SubmenuIndicatorInstance { get; protected set; }
        public ContainerRuntime ContainerInstance { get; protected set; }
        public ContainerRuntime SubItemContainerInstance { get; protected set; }

        public string Header
        {
            get => TextInstance.Text;
            set => TextInstance.Text = value;
        }

        public MenuItemRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/MenuItem");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.MenuItem(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::Gum.GueDeriving.TextRuntime;
            SubmenuIndicatorInstance = this.GetGraphicalUiElementByName("SubmenuIndicatorInstance") as global::Gum.GueDeriving.TextRuntime;
            ContainerInstance = this.GetGraphicalUiElementByName("ContainerInstance") as global::Gum.GueDeriving.ContainerRuntime;
            SubItemContainerInstance = this.GetGraphicalUiElementByName("SubItemContainerInstance") as global::Gum.GueDeriving.ContainerRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
