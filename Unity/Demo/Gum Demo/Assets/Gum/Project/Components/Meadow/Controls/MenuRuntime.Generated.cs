//Code for Meadow/Controls/Menu (Container)
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
    partial class MenuRuntime : ContainerRuntime
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/Menu", typeof(MenuRuntime));
            global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(global::Gum.Forms.Controls.Menu)] = new global::Gum.Forms.VisualTemplate((vm, createForms) => new MenuRuntime(fullInstantiation: true, tryCreateFormsObject: createForms));
        }
        public global::Gum.Forms.Controls.Menu FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.Menu;
        public RectangleRuntime Background { get; protected set; }
        public RectangleRuntime BottomSeparator { get; protected set; }
        public ContainerRuntime InnerPanelInstance { get; protected set; }

        public MenuRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/Menu");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.Menu(this);
            }
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            BottomSeparator = this.GetGraphicalUiElementByName("BottomSeparator") as global::Gum.GueDeriving.RectangleRuntime;
            InnerPanelInstance = this.GetGraphicalUiElementByName("InnerPanelInstance") as global::Gum.GueDeriving.ContainerRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
