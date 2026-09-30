//Code for Meadow/Controls/UserControl (Container)
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
    partial class UserControlRuntime : ContainerRuntime
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/UserControl", typeof(UserControlRuntime));
        }
        public RectangleRuntime Background { get; protected set; }

        public UserControlRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/UserControl");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            Background = this.GetGraphicalUiElementByName("Background") as global::Gum.GueDeriving.RectangleRuntime;
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
