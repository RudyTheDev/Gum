//Code for Meadow/Controls/ScrollViewer (Container)
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
    partial class ScrollViewerRuntime : ContainerRuntime
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/ScrollViewer", typeof(ScrollViewerRuntime));
            global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(global::Gum.Forms.Controls.ScrollViewer)] = new global::Gum.Forms.VisualTemplate((vm, createForms) => new ScrollViewerRuntime(fullInstantiation: true, tryCreateFormsObject: createForms));
        }
        public global::Gum.Forms.Controls.ScrollViewer FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.ScrollViewer;
        public enum ScrollBarVisibility
        {
            NoScrollBar,
            VerticalScrollVisible,
            HorizontalScrollVisible,
            BothScrollVisible,
        }
        public enum ScrollViewerCategory
        {
            Enabled,
            Focused,
        }

        ScrollBarVisibility? _scrollBarVisibilityState;
        public ScrollBarVisibility? ScrollBarVisibilityState
        {
            get => _scrollBarVisibilityState;
            set
            {
                _scrollBarVisibilityState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("ScrollBarVisibility"))
                    {
                        var category = Categories["ScrollBarVisibility"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ScrollBarVisibility");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }

        ScrollViewerCategory? _scrollViewerCategoryState;
        public ScrollViewerCategory? ScrollViewerCategoryState
        {
            get => _scrollViewerCategoryState;
            set
            {
                _scrollViewerCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("ScrollViewerCategory"))
                    {
                        var category = Categories["ScrollViewerCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "ScrollViewerCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }
        public RectangleRuntime Background { get; protected set; }
        public ScrollBarRuntime VerticalScrollBarInstance { get; protected set; }
        public ScrollBarRuntime HorizontalScrollBarInstance { get; protected set; }
        public ContainerRuntime ClipContainerInstance { get; protected set; }
        public ContainerRuntime InnerPanelInstance { get; protected set; }
        public RectangleRuntime BorderInstance { get; protected set; }
        public RectangleRuntime FocusedIndicator { get; protected set; }

        public ScrollViewerRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/ScrollViewer");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.ScrollViewer(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            VerticalScrollBarInstance = this.GetGraphicalUiElementByName("VerticalScrollBarInstance") as Assembly_CSharp.Components.Meadow.Controls.ScrollBarRuntime;
            HorizontalScrollBarInstance = this.GetGraphicalUiElementByName("HorizontalScrollBarInstance") as Assembly_CSharp.Components.Meadow.Controls.ScrollBarRuntime;
            ClipContainerInstance = this.GetGraphicalUiElementByName("ClipContainerInstance") as global::Gum.GueDeriving.ContainerRuntime;
            InnerPanelInstance = this.GetGraphicalUiElementByName("InnerPanelInstance") as global::Gum.GueDeriving.ContainerRuntime;
            BorderInstance = this.GetGraphicalUiElementByName("BorderInstance") as global::Gum.GueDeriving.RectangleRuntime;
            FocusedIndicator = this.GetGraphicalUiElementByName("FocusedIndicator") as global::Gum.GueDeriving.RectangleRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
