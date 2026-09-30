//Code for Meadow/Controls/ItemsControl (Container)
using Assembly_CSharp.Components.Meadow.Controls;
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
    partial class ItemsControlRuntime : ContainerRuntime
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ItemsControl", typeof(ItemsControlRuntime));
        }
        public global::Gum.Forms.Controls.ItemsControl FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ItemsControl;
        public enum ItemsControlCategory
        {
            Enabled,
            Disabled,
            Focused,
            DisabledFocused,
        }

        ItemsControlCategory? _itemsControlCategoryState;
        public ItemsControlCategory? ItemsControlCategoryState
        {
            get => _itemsControlCategoryState;
            set
            {
                _itemsControlCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("ItemsControlCategory"))
                    {
                        var category = Categories["ItemsControlCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ItemsControlCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }
        public RectangleRuntime Background { get; protected set; }
        public ScrollBarRuntime VerticalScrollBarInstance { get; protected set; }
        public ContainerRuntime ClipContainerInstance { get; protected set; }
        public ContainerRuntime InnerPanelInstance { get; protected set; }
        public ContainerRuntime ClipAndScrollContainer { get; protected set; }
        public ContainerRuntime ClipContainerParent { get; protected set; }
        public RectangleRuntime FocusedIndicator { get; protected set; }

        public ItemsControlRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ItemsControl");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.ItemsControl(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            VerticalScrollBarInstance = this.GetGraphicalUiElementByName("VerticalScrollBarInstance") as Assembly_CSharp.Components.Meadow.Controls.ScrollBarRuntime;
            ClipContainerInstance = this.GetGraphicalUiElementByName("ClipContainerInstance") as global::Gum.GueDeriving.ContainerRuntime;
            InnerPanelInstance = this.GetGraphicalUiElementByName("InnerPanelInstance") as global::Gum.GueDeriving.ContainerRuntime;
            ClipAndScrollContainer = this.GetGraphicalUiElementByName("ClipAndScrollContainer") as global::Gum.GueDeriving.ContainerRuntime;
            ClipContainerParent = this.GetGraphicalUiElementByName("ClipContainerParent") as global::Gum.GueDeriving.ContainerRuntime;
            FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::Gum.GueDeriving.RectangleRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
