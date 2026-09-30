//Code for Meadow/Controls/ComboBox (Container)
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
    partial class ComboBoxRuntime : ContainerRuntime
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ComboBox", typeof(ComboBoxRuntime));
            global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.ComboBox)] = typeof(ComboBoxRuntime);
        }
        public global::Gum.Forms.Controls.ComboBox FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ComboBox;
        public enum ComboBoxCategory
        {
            Enabled,
            Disabled,
            Highlighted,
            Pushed,
            HighlightedFocused,
            Focused,
            DisabledFocused,
        }

        ComboBoxCategory? _comboBoxCategoryState;
        public ComboBoxCategory? ComboBoxCategoryState
        {
            get => _comboBoxCategoryState;
            set
            {
                _comboBoxCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("ComboBoxCategory"))
                    {
                        var category = Categories["ComboBoxCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ComboBoxCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }
        public RectangleRuntime Background { get; protected set; }
        public TextRuntime TextInstance { get; protected set; }
        public ListBoxRuntime ListBoxInstance { get; protected set; }
        public TextRuntime IconInstance { get; protected set; }
        public RectangleRuntime BorderInstance { get; protected set; }
        public RectangleRuntime FocusedIndicator { get; protected set; }

        public ComboBoxRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ComboBox");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.ComboBox(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            TextInstance = this.GetGraphicalUiElementByName("TextInstance") as global::Gum.GueDeriving.TextRuntime;
            ListBoxInstance = this.GetGraphicalUiElementByName("ListBoxInstance") as Assembly_CSharp.Components.Meadow.Controls.ListBoxRuntime;
            IconInstance = this.GetGraphicalUiElementByName("IconInstance") as global::Gum.GueDeriving.TextRuntime;
            BorderInstance = this.GetGraphicalUiElementByName("BorderInstance") as global::Gum.GueDeriving.RectangleRuntime;
            FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::Gum.GueDeriving.RectangleRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
