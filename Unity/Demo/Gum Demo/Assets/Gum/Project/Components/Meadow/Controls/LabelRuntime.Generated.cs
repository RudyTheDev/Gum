//Code for Meadow/Controls/Label (Text)
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
    partial class LabelRuntime : global::Gum.GueDeriving.TextRuntime
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void RegisterRuntimeType()
        {
            GumRuntime.ElementSaveExtensions.RegisterGueInstantiationType("Meadow/Controls/Label", typeof(LabelRuntime));
        }
        public global::Gum.Forms.Controls.Label FormsControl => FormsControlAsObject as global::Gum.Forms.Controls.Label;
        public enum TextCategory
        {
            Normal,
            Strong,
        }

        TextCategory? _textCategoryState;
        public TextCategory? TextCategoryState
        {
            get => _textCategoryState;
            set
            {
                _textCategoryState = value;
                if(value != null)
                {
                    if(Categories.ContainsKey("TextCategory"))
                    {
                        var category = Categories["TextCategory"];
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                    else
                    {
                        var category = ((global::Gum.DataTypes.ElementSave)this.Tag).Categories.FirstOrDefault(item => item.Name == "TextCategory");
                        var state = category.States.Find(item => item.Name == value.ToString());
                        this.ApplyState(state);
                    }
                }
            }
        }

        public LabelRuntime(bool fullInstantiation = true, bool tryCreateFormsObject = true)
        {
            if(fullInstantiation)
            {
                var element = ObjectFinder.Self.GetElementSave("Meadow/Controls/Label");
                element?.SetGraphicalUiElement(this, global::RenderingLibrary.SystemManagers.Default);
            }



        }
        public override void AfterFullCreation()
        {
            if (FormsControl == null)
            {
                FormsControlAsObject = new global::Gum.Forms.Controls.Label(this);
            }
            CustomInitialize();
        }
        //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
        partial void CustomInitialize();
    }
}
