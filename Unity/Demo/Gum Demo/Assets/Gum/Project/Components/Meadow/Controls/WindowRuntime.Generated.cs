//Code for Meadow/Controls/Window (Container)
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
    partial class WindowRuntime : ContainerRuntime
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/Window", typeof(WindowRuntime));
        }
        public global::Gum.Forms.Window FormsControl => FormsControlAsObject as global::Gum.Forms.Window;
        public RectangleRuntime Background { get; protected set; }
        public RectangleRuntime BorderInstance { get; protected set; }
        public PanelRuntime InnerPanelInstance { get; protected set; }
        public PanelRuntime TitleBarInstance { get; protected set; }
        public RectangleRuntime TitleBarFill { get; protected set; }
        public RectangleRuntime TitleBarSeparator { get; protected set; }
        public PanelRuntime BorderTopLeftInstance { get; protected set; }
        public PanelRuntime BorderTopRightInstance { get; protected set; }
        public PanelRuntime BorderBottomLeftInstance { get; protected set; }
        public PanelRuntime BorderBottomRightInstance { get; protected set; }
        public PanelRuntime BorderTopInstance { get; protected set; }
        public PanelRuntime BorderBottomInstance { get; protected set; }
        public PanelRuntime BorderLeftInstance { get; protected set; }
        public PanelRuntime BorderRightInstance { get; protected set; }

        public WindowRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/Window");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Window(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            BorderInstance = this.GetGraphicalUiElementByName("BorderInstance") as global::Gum.GueDeriving.RectangleRuntime;
            InnerPanelInstance = this.GetGraphicalUiElementByName("InnerPanelInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            TitleBarInstance = this.GetGraphicalUiElementByName("TitleBarInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            TitleBarFill = this.GetGraphicalUiElementByName("TitleBarFill") as global::Gum.GueDeriving.RectangleRuntime;
            TitleBarSeparator = this.GetGraphicalUiElementByName("TitleBarSeparator") as global::Gum.GueDeriving.RectangleRuntime;
            BorderTopLeftInstance = this.GetGraphicalUiElementByName("BorderTopLeftInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderTopRightInstance = this.GetGraphicalUiElementByName("BorderTopRightInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderBottomLeftInstance = this.GetGraphicalUiElementByName("BorderBottomLeftInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderBottomRightInstance = this.GetGraphicalUiElementByName("BorderBottomRightInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderTopInstance = this.GetGraphicalUiElementByName("BorderTopInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderBottomInstance = this.GetGraphicalUiElementByName("BorderBottomInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderLeftInstance = this.GetGraphicalUiElementByName("BorderLeftInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            BorderRightInstance = this.GetGraphicalUiElementByName("BorderRightInstance") as Assembly_CSharp.Components.Meadow.Controls.PanelRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
