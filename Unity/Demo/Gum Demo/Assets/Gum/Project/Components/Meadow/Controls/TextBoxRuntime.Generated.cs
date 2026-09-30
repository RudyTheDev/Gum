//Code for Meadow/Controls/TextBox (Container)
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
    partial class TextBoxRuntime : ContainerRuntime
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/TextBox", typeof(TextBoxRuntime));
            global::Gum.Forms.Controls.FrameworkElement.DefaultFormsComponents[typeof(global::Gum.Forms.Controls.TextBox)] = typeof(TextBoxRuntime);
        }
        public global::Gum.Forms.Controls.TextBox FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.TextBox;
        public enum TextBoxCategory
        {
            Enabled,
            Highlighted,
            Focused,
            Disabled,
        }
        public enum LineModeCategory
        {
            Single,
            Multi,
        }

        TextBoxCategory? _textBoxCategoryState;
        public TextBoxCategory? TextBoxCategoryState
        {
            get => _textBoxCategoryState;
            set
            {
                _textBoxCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("TextBoxCategory"))
                    {
                        var category = Categories["TextBoxCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "TextBoxCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }

        LineModeCategory? _lineModeCategoryState;
        public LineModeCategory? LineModeCategoryState
        {
            get => _lineModeCategoryState;
            set
            {
                _lineModeCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("LineModeCategory"))
                    {
                        var category = Categories["LineModeCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "LineModeCategory");
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

        public int? MaxNumberOfLines
        {
            get => TextInstance.MaxNumberOfLines;
            set => TextInstance.MaxNumberOfLines = value;
        }

        public string Text
        {
            get => TextInstance.Text;
            set => TextInstance.Text = value;
        }

        public TextBoxRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/TextBox");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.TextBox(this);
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
}
